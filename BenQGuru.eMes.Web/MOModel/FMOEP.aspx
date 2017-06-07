<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" Codebehind="FMOEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FMOEP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>FMOEP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">

    <script language="javascript">
			//设置只读
			function SetReadOnly(cb)
			{
				if(cb.checked == true)
				{	
					document.all.txtBIOSVersion.readOnly  = false;
					
				}
				else
				{
					document.all.txtBIOSVersion.readOnly = true;
				}
			}
    </script>

</head>
<body>
    <form id="Form2" method="post" runat="server">
        <table id="Table1" height="100%" width="100%">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单详细信息</asp:Label></td>
            </tr>
            <tr>
                <td class="fieldGrid">
                    <table class="edit" id="Table3" cellspacing="1" cellpadding="1" border="0">
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblFactoryEdit" runat="server">工厂</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtFactoryEdit" runat="server" CssClass="textbox" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMOEdit" runat="server">工单</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMOEdit" runat="server" CssClass="require" Width="100px" ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblItemEdit" runat="server">产品</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtItemCodeEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMOTypeEdit" runat="server">工单类型</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:DropDownList ID="drpMOTypeEdit" runat="server" CssClass="require" Width="100px"
                                    Enabled="False">
                                </asp:DropDownList></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOStatusEdit" runat="server">生产状态</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:DropDownList ID="drpMOStatusEdit" runat="server" CssClass="require" Width="100px"
                                    Enabled="False">
                                </asp:DropDownList></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblCustomerCodeEdit" runat="server">客户</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtCustomerCodeEdit" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblCustomerOrderNOEdit" runat="server" DESIGNTIMEDRAGDROP="957">客户单号</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtCustomerOrderNOEdit" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMOMemoGroup" runat="server">工单别</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="textbox" Width="100px" ></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblProduceRoute" runat="server">生产途程</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:DropDownList ID="drpRouteEdit" runat="server" Width="100px" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td class="fieldValue" nowrap>
                            </td>
                            <td nowrap>
                                <asp:CheckBox ID="chbLimitItemQtyEdit" runat="server" Text="限制产品投入量"></asp:CheckBox></td>
                            <td class="toolBar" nowrap colspan="1">
                                <input language="javascript" class="submitImgButton" id="cmdBOM" type="submit" value="比对BOM"
                                    name="cmdAdd" runat="server" onserverclick="cmdAdd_ServerClick"></td>
                            <td class="fieldValue" nowrap>
                                <asp:Label ID="lblCompareBOMInformation" runat="server">已投入产量</asp:Label></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMOBomEdit" runat="server">工单Bom</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMOBomEdit" runat="server" CssClass="require" Width="100px"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOQtyEdit" runat="server">工单数量</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMOQtyEdit" runat="server" CssClass="require" Width="100px" ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblInputQtyEdit" runat="server">已投入产量</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtInputQtyEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblCompleteQtyEdit" runat="server">已完工数量</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtCompleteQtyEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblScrapeQtyEdit" runat="server">报废数量</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtScrapeQtyEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" style="height: 25px" nowrap>
                                <asp:Label ID="lblUnCompleteQtyEdit" runat="server">未完工数量</asp:Label></td>
                            <td class="fieldValue" style="height: 25px" nowrap align="left">
                                <asp:TextBox ID="txtUnCompleteQtyEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblOffmoqtyEdit" runat="server">脱离工单数量</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtOffMOQtyEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMergeRule" runat="server"> 转换比例</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtDenominatorEdit" runat="server" CssClass="require" Width="100px"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblRMABillNo" runat="server"> RMA单号</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtRMABillNo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:CheckBox ID="chbBIOSVersion" runat="server" Text=""></asp:CheckBox><asp:Label
                                    ID="lblBIOSVersion" runat="server">比对软件版本</asp:Label></td>
                            <td class="fieldValue" nowrap colspan="5">
                                <asp:TextBox ID="txtBIOSVersion" runat="server" CssClass="textbox" Width="100%"></asp:TextBox></td>
                            <td class="fieldName" nowrap style="display: none">
                                <asp:Label ID="lblPCBAVersion" runat="server">PCBA版本</asp:Label></td>
                            <td class="fieldValue" nowrap style="display: none">
                                <asp:TextBox ID="txtPCBAVersion" runat="server" Width="100px"></asp:TextBox></td>
                            <td colspan="2">
                            </td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblPlanStartDateEdit" runat="server">预计开始时间</asp:Label></td>
                            <td class="fieldValue">
                                <uc1:emesdate id="txtPlanStartDateEdit" cssclass="textbox" width="100" runat="server"></uc1:emesdate>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblPlanEndDateEdit" runat="server">预计完成时间</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <uc1:emesdate id="txtPlanEndDateEdit" runat="server" cssclass="textbox" width="100"></uc1:emesdate>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblActualStartDateEdit" runat="server">实际开始时间</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtActualStartDateEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblActualEndDateEdit" runat="server">实际完成时间</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtActualEndDateEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOUserEdit" runat="server" DESIGNTIMEDRAGDROP="959">开单人员</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMOUserEdit" runat="server" CssClass="textbox" Width="100px" ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName">
                                <asp:Label ID="lblMODownloadDateEdit" runat="server" DESIGNTIMEDRAGDROP="960">开单日期</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtMODownloadDateEdit" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMaintainUser" runat="server">上次维护人员</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMaintainUser" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMaintainDate" runat="server">上次维护时间</asp:Label></td>
                            <td class="fieldValue" nowrap>
                                <asp:TextBox ID="txtMaintainDate" runat="server" CssClass="require" Width="100px"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblPendingCause" runat="server">暂停原因</asp:Label></td>
                            <td class="fieldValue" colspan="3">
                                <asp:TextBox ID="txtPendingCause" runat="server" CssClass="textbox" Width="330px"
                                    Height="40px" MaxLength="50" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                                    
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMORemarkEdit" runat="server">工单备注</asp:Label></td>
                            <td class="fieldValue" colspan="3">
                                <asp:TextBox ID="txtMORemarkEdit" runat="server" CssClass="textbox" Width="330px"
                                    Height="40px" MaxLength="50" TextMode="MultiLine" Rows="2"></asp:TextBox></td>        
                            <td colspan="2">
                            </td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap valign="top">
                                <asp:Label ID="lblMORCardRange" runat="server">DCT采集防呆</asp:Label></td>
                            <td class="fieldValue" colspan="5" height="100">
                                <igtbl:ultrawebgrid id="gridWebGrid" runat="server" width="100%" height="100">
										<DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
											AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
											SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
											AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault="">
											<ADDNEWBOX>
												<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
											</ADDNEWBOX>
											<PAGER>
												<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
											</PAGER>
											<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
												BorderColor="White" Font-Bold="True" Font-Size="12px">
												<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
													ColorTop="White"></BORDERDETAILS>
											</HEADERSTYLEDEFAULT>
											<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
											<FRAMESTYLE Height="100%" Width="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
												Font-Size="12px" Font-Names="Verdana"></FRAMESTYLE>
											<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
												<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
											</FOOTERSTYLEDEFAULT>
											<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
											<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
												<PADDING Bottom="1px"></PADDING>
											</EDITCELLSTYLEDEFAULT>
											<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
											<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
												VerticalAlign="Middle">
												<PADDING Left="3px"></PADDING>
												<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
											</ROWSTYLEDEFAULT>
											<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
										</DISPLAYLAYOUT>
										<BANDS>
											<igtbl:UltraGridBand></igtbl:UltraGridBand>
										</BANDS>
									</igtbl:ultrawebgrid>
                            </td>
                            <td class="fieldNameLeft" nowrap valign="top">
                                <input class="submitImgButton" id="cmdAddLot" type="submit" value="新 增" name="cmdAddLot"
                                    runat="server" onserverclick="cmdAddLot_ServerClick">
                            </td>
                            <td colspan="3">
                            </td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="fieldGrid">
                    <table class="edit" height="100%" width="100%">
                    </table>
                </td>
            </tr>
            <tr>
                <td class="fieldGrid">
                    <table class="edit" height="100%" width="100%">
                    </table>
                </td>
            </tr>
            <tr>
                <td nowrap height="100%">
                </td>
            </tr>
            <tr class="toolBar">
                <td>
                    <table class="toolBar" id="Table4">
                        <tr>
                            <td class="toolBar">
                                <input language="javascript" class="submitImgButton" id="cmdSave" type="submit" value="保 存"
                                    name="cmdAdd" runat="server" onserverclick="cmdSave_ServerClick"></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdCancel_ServerClick"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
