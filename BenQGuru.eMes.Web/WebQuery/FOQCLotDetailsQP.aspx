<%@ Page Language="c#" CodeBehind="FOQCLotDetailsQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FOQCLotDetailsQP" %>

<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FINNOInfoQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function onCheckBoxChange(cb) {
            if (cb.id == "chbItemDetail") {
                document.all.chbRepairDetail.checked = false;
            }
            else if (cb.id == "chbRepairDetail") {
                document.all.chbItemDetail.checked = false;
            }
            else if (cb.id == "chbOQCDate") {
                document.all.rdbPackedDate.checked = false;
            }
        }

        function onRadioCheckChange(radio) {
            if (radio.id == "rdbPackedDate") {
                document.all.chbOQCDate.checked = false;
            }
            //				else if(radio.id == "rdbOQCDate")
            //				{
            //					document.all.rdbPackedDate.checked = false;
            //				}
        }

        function OnOQCLotKeyUp() {

            if (document.getElementById("txtOQCLotQuery").value == "") {
                //alert(document.getElementById("chbQueryHistory").disabled );
                document.getElementById("chbQueryHistory").checked = false;
                document.getElementById("chbQueryHistory").disabled = "disabled";
            }
            else {
                //alert(document.getElementById("txtOQCLotQuery").value+":-:"+document.getElementById("cbQueryHistory").disabled );
                document.getElementById("chbQueryHistory").disabled = "";
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" onload="OnOQCLotKeyUp();">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td style="height: 19px">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">送检批明细查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" id="Table2" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" type="item" width="90px"
                                cankeyin="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" type="mo" width="90px"
                                cankeyin="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOQCLotQuery" runat="server">送检批号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtOQCLotQuery" runat="server" CssClass="textbox" Width="130"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStatus" runat="server">状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpOQCStateQuery" runat="server" Width="130" DESIGNTIMEDRAGDROP="566">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartRCardQuery" runat="server">起始产品序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtStartSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndRCardQuery" runat="server">截止产品序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEndSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStepSequence" runat="server">生产线</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtStepSequence" runat="server" width="90px" type="stepsequence"
                                target="stepsequence" cankeyin="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblProductionType" runat="server">生产类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtProductionTypeQuery" runat="server" type="productiontype"
                                readonly="false" cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCrewCodeQuery" runat="server">班组</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpCrewCodeQuery" runat="server" Width="130px" OnLoad="drpCrewCodeQuery_Load">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMDateQuery" runat="server">维护日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                        <asp:TextBox  id="dateInDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTo" runat="server"> 到</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox  id="dateInDateToQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="fieldName" nowrap>
                            <asp:RadioButton ID="rdbPackedDate" runat="server"></asp:RadioButton><asp:Label ID="lblPackingBeginDate"
                                runat="server">包装起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="txtPackedBeginDate"  class='datepicker' runat="server"  Width="80px"/>
                                    </td>
                                    <td>
                                        <uc1:emestime id="txtPackedBeginTime" runat="server" width="60"></uc1:emestime>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPackedEnddate" runat="server">包装结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="txtPackedEndDate"  class='datepicker' runat="server"  Width="80px"/>
                                    </td>
                                    <td>
                                        <uc1:emestime id="txtPackedEndTime" runat="server" width="60"></uc1:emestime>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDecidedate" runat="server">判定日期</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:CheckBox ID="chbOQCDate" runat="server" Checked="true" Enabled="false"></asp:CheckBox><asp:Label
                                ID="lblGenerateLotBegdate" runat="server">产生批起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:TextBox  id="txtOQCBeginDate"  class='datepicker' runat="server"  Width="130px"/>
                                    </td>
                                    <%--<TD><uc1:emestime id="txtOQCBeginTime" runat="server" width="60"></uc1:emestime></TD>--%>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblGenerateLotEnddate" runat="server">产生批结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="txtOQCEndDate"  class='datepicker' runat="server"  Width="130px"/>
                                    </td>
                                    <%--<TD><uc1:emestime id="txtOQCEndTime" runat="server" width="60"></uc1:emestime></TD>--%>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBigSSCodeWhere" runat="server">大线</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtBigSSCodeWhere" runat="server" type="bigline" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFirstClassGroup" runat="server">一级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpFirstClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="true" OnLoad="drpFirstClass_Load" OnSelectedIndexChanged="drpFirstClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSecondClassGroup" runat="server">二级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpSecondClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="true" OnSelectedIndexChanged="drpSecondClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblThirdClassGroup" runat="server">三级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpThirdClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDecideBegdate" runat="server">判定起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="txtDecideBegdateQuery"  class='datepicker' runat="server"  Width="130px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDecideEnddate" runat="server">判定结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="txtDecideEnddateQuery"  class='datepicker' runat="server"  Width="130px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialModelCodeWhere" runat="server">机型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtMaterialModelCodeWhere" runat="server" type="mmodelcode"
                                readonly="false" cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialMachineTypeWhere" runat="server">整机机芯</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtMaterialMachineTypeWhere" runat="server" type="mmachinetype"
                                readonly="false" cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialExportImportWhere" runat="server" Text="内销/出口">内销/出口</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpMaterialExportImportWhere" runat="server" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDecideManQuery" runat="server">判定人</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtDecideManQuery" runat="server" type="user" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td style="display: none">
                            <asp:CheckBox ID="chbQueryHistory" runat="server" Text="查询历史批"></asp:CheckBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOQCLotType" runat="server">批类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtOQCLotTypeQuery" runat="server" type="oqclottype" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReWorkMO" runat="server">返工需求单号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReWorkMOQuery" runat="server" CssClass="textbox" Width="130"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                                </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
                <displaylayout colwidthdefault="" stationarymargins="Header" allowsortingdefault="Yes"
                    rowheightdefault="20px" version="2.00" selecttyperowdefault="Single" selecttypecelldefault="Single"
                    headerclickactiondefault="SortSingle" bordercollapsedefault="Separate" allowcolsizingdefault="Free"
                    cellpaddingdefault="4" rowselectorsdefault="No" name="webGrid" tablelayout="Fixed"><ADDNEWBOX>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								    
								</STYLE>
							</ADDNEWBOX>
							<PAGER>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								    
								</STYLE>
							</PAGER>
							<HEADERSTYLEDEFAULT BorderWidth="1px" BorderStyle="Dashed" BackColor="#ABABAB" Font-Size="12px" Font-Bold="True"
								BorderColor="White" HorizontalAlign="Left">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
									ColorLeft="White"></BORDERDETAILS>
							</HEADERSTYLEDEFAULT>
							<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
							<FRAMESTYLE Width="100%" Height="100%" BorderWidth="0px" BorderStyle="Groove" Font-Size="12px"
								BorderColor="#ABABAB" Font-Names="Verdana"></FRAMESTYLE>
							<FOOTERSTYLEDEFAULT BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BORDERDETAILS>
							</FOOTERSTYLEDEFAULT>
							<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
							<EDITCELLSTYLEDEFAULT BorderWidth="1px" BorderStyle="None" BorderColor="Black" VerticalAlign="Middle">
								<PADDING Bottom="1px"></PADDING>
							</EDITCELLSTYLEDEFAULT>
							<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
							<ROWSTYLEDEFAULT BorderWidth="1px" BorderStyle="Solid" BorderColor="#D7D8D9" HorizontalAlign="Left"
								VerticalAlign="Middle">
								<PADDING Left="3px"></PADDING>
								<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
							</ROWSTYLEDEFAULT>
							<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
						</displaylayout>
                <bands><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
						</bands>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table id="Table3" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td width="140">
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                            
                        </td>
                        <td width="100" style="display: none;">
                            <asp:CheckBox ID="chbItemDetail" runat="server" Text="导出产品明细"></asp:CheckBox>
                        </td>
                        <td width="120">
                            <asp:CheckBox ID="chbRepairDetail" runat="server" Text="导出维修明细"></asp:CheckBox>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
