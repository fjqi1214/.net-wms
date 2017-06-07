<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FProductivity.aspx.cs" Inherits="BenQGuru.Web.ReportCenter.KPI.Productivity" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
    <title>FLineBalanceRate</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="<%=StyleSheet%>" rel=stylesheet>
</HEAD>
<body>
<form id="form1" method="post" runat="server">
    <table height="100%" width="100%" id="Table1">
    <tr class="moduleTitle">
		<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">生产率</asp:label></td>
	</tr>
	<tr>
		<td>
			<table class="query" height="100%" width="100%">
				<tr>
					<td class="fieldName" noWrap><asp:label id="lblMOCodeQuery" runat="server">工单号</asp:label></td>
					<td class="fieldValue"><cc2:selectabletextbox id="txtMOCodeWhere" runat="server" Type="singlemo" Target="singlemo" Readonly="false" CanKeyIn="true" width="131"></cc2:selectabletextbox></td>
					
                    <TD class="fieldName" noWrap><asp:label id="lblSS" runat="server">生产线</asp:label></TD>
					<TD class="fieldValue" noWrap><cc2:selectabletextbox4SS id="txtSSCodeWhere" runat="server" Type="singlestepsequence" Readonly="false" CanKeyIn="true" Width="131"></cc2:selectabletextbox4SS></TD>
					<TD class="fieldNameLeft" noWrap><asp:label id="lblSDateQuery" runat="server">开始时间</asp:label></TD>
					<td class="fieldValue" noWrap>
						 <asp:TextBox  id="dateStartDateQuery"  class='datepicker require' runat="server"  Width="131px"/>
					</td>
                </tr>
				<tr>
				    
					<td class="fieldNameLeft" noWrap><asp:label id="lblEDateQuery" runat="server">结束时间</asp:label></td>
					<td class="fieldValue" noWrap>
					<asp:TextBox  id="dateEndDateQuery"  class='datepicker require' runat="server"  Width="131px"/>
					</td>
					<TD class="fieldName" noWrap><asp:label id="lblShiftCodeWhere" runat="server">班次</asp:label></TD>
					<TD class="fieldValue" noWrap><asp:DropDownList ID="ddlShiftCodeWhere" 
                            runat="server" Width="131" AutoPostBack="true"
                            onselectedindexchanged="ddlShiftCodeWhere_SelectedIndexChanged" ></asp:DropDownList></TD>
					<TD class="fieldName" noWrap><asp:label id="lblTimePeriod" runat="server" >时段</asp:label></TD>
					<TD class="fieldValue" noWrap><asp:DropDownList ID="ddlMoTypeQuery" runat="server" Width="131" ></asp:DropDownList></TD>
					
					<td></td>
					<td></td>
					<TD style="PADDING-RIGHT: 8px" align="right"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
							runat="server" onserverclick="cmdQuery_ServerClick"></TD>
				</TR>
			</table>
		</td>
	</tr>
	<tr height="100%">
	        <td align="center" >
	            <iggauge:ultragauge runat="server" ID="UltraGauge1" BackColor="Transparent" 
                    ForeColor="ControlLightLight" Width="350" Height="350" 
                    OnAsyncRefresh="UltraGauge1_AsyncRefresh">
                  <%--<DeploymentScenario DeleteOldImages="true" FilePath="~/Temp/GaugeImages"
                    ImageURL="~/Temp/GaugeImages/Gauge_#SEQNUM(100).#EXT" />--%>
                  <%--<DeploymentScenario Mode="Session" />--%>
                  <Gauges>
                    <igGaugeProp:RadialGauge >
                      <OverDial>
                        <BrushElements>
                          <igGaugeProp:BrushElementGroup>
                            <BrushElements>
                              <igGaugeProp:MultiStopRadialGradientBrushElement FocusScalesString="5, 0" CenterPointString="8, 100">
                                <ColorStops>
                                  <igGaugeProp:ColorStop Color="50, 255, 255, 255" />
                                  <igGaugeProp:ColorStop Color="150, 255, 255, 255" Stop="0.3310345" />
                                  <igGaugeProp:ColorStop Color="Transparent" Stop="0.3359606" />
                                  <igGaugeProp:ColorStop Color="0, 255, 255, 255" Stop="1" />
                                </ColorStops>
                              </igGaugeProp:MultiStopRadialGradientBrushElement>
                            </BrushElements>
                          </igGaugeProp:BrushElementGroup>
                        </BrushElements>
                      </OverDial>
                      <Scales>
                        <igGaugeProp:RadialGaugeScale EndAngle="405" StartAngle="135">
                          <MinorTickmarks EndWidth="1" EndExtent="78" Frequency="2" StartExtent="73">
                            <BrushElements>
                              <igGaugeProp:SolidFillBrushElement Color="240, 240, 240" />
                            </BrushElements>
                            <StrokeElement>
                              <BrushElements>
                                <igGaugeProp:SolidFillBrushElement Color="135, 135, 135" />
                              </BrushElements>
                            </StrokeElement>
                          </MinorTickmarks>
                          <Ranges>
                            <%--<igGaugeProp:RadialGaugeRange InnerExtentStart="50" InnerExtentEnd="50" EndValue="100" OuterExtent="63" StartValue="0">
                                <BrushElements>
                                    <igGaugeProp:HatchBrushElement HatchStyle="Percent80" BackColor="150, 255, 255, 255" ForeColor="Transparent"/>
                                </BrushElements>
                            <StrokeElement Color="180, 180, 180">
                                <BrushElements>
                                    <igGaugeProp:SolidFillBrushElement Color="50, 255, 255, 255"/>
                                </BrushElements>
                            </StrokeElement>
                            </igGaugeProp:RadialGaugeRange>--%>
                            <igGaugeProp:RadialGaugeRange InnerExtentStart="62" InnerExtentEnd="62" OuterExtent="70">
                                <BrushElements>
                                    <igGaugeProp:SolidFillBrushElement Color="Red"/>
                                </BrushElements>
                            </igGaugeProp:RadialGaugeRange>
                            <igGaugeProp:RadialGaugeRange InnerExtentStart="62" InnerExtentEnd="62" OuterExtent="70">
                                <BrushElements>
                                    <igGaugeProp:SolidFillBrushElement Color="Yellow"/>
                                </BrushElements>
                            </igGaugeProp:RadialGaugeRange>
                            <igGaugeProp:RadialGaugeRange InnerExtentStart="62" InnerExtentEnd="62" OuterExtent="70">
                                <BrushElements>
                                    <igGaugeProp:SolidFillBrushElement Color="Green"/>
                                </BrushElements>
                            </igGaugeProp:RadialGaugeRange>
                          </Ranges>
                          <Markers>
                            <igGaugeProp:RadialGaugeNeedle MidWidth="5" EndWidth="3" MidExtent="0" EndExtent="65" StartExtent="-20" StartWidth="5">
                              <Anchor  RadiusMeasure="Percent" Radius="9">
                                <BrushElements>
                                  <igGaugeProp:SimpleGradientBrushElement EndColor="64, 64, 64" StartColor="Gainsboro" GradientStyle="BackwardDiagonal" />
                                </BrushElements>
                                <StrokeElement Thickness="2">
                                  <BrushElements>
                                    <igGaugeProp:RadialGradientBrushElement FocusScalesString="0, 0" CenterPointString="75, 25" SurroundColor="Gray" CenterColor="WhiteSmoke" />
                                  </BrushElements>
                                </StrokeElement>
                              </Anchor>
                              <BrushElements>
                                <igGaugeProp:SolidFillBrushElement Color="255, 61, 22" />
                              </BrushElements>
                              <StrokeElement Thickness="0" />
                            </igGaugeProp:RadialGaugeNeedle>
                          </Markers>
                          <Axes>
                            <igGaugeProp:NumericAxis EndValue="100" />
                          </Axes>
                          <MajorTickmarks EndWidth="3" EndExtent="79" Frequency="10" StartExtent="67" StartWidth="3">
                            <BrushElements>
                              <igGaugeProp:SolidFillBrushElement Color="Gray" />
                            </BrushElements>
                          </MajorTickmarks>
                          <Labels Orientation="Horizontal" Frequency="20" Extent="55" Font="Arial, 8pt, style=Bold">
                            <BrushElements>
                              <igGaugeProp:SolidFillBrushElement Color="64, 64, 64" />
                            </BrushElements>
                          </Labels>
                        </igGaugeProp:RadialGaugeScale>
                      </Scales>
                      <Dial>
                        <BrushElements>
                          <igGaugeProp:BrushElementGroup>
                            <BrushElements>
                              <igGaugeProp:MultiStopRadialGradientBrushElement FocusScalesString="0.8, 0.8" CenterPointString="50, 50">
                                <ColorStops>
                                  <igGaugeProp:ColorStop Color="240, 240, 240" />
                                  <igGaugeProp:ColorStop Color="195, 195, 195" Stop="0.3413793" />
                                  <igGaugeProp:ColorStop Color="195, 195, 195" Stop="1" />
                                </ColorStops>
                              </igGaugeProp:MultiStopRadialGradientBrushElement>
                              <igGaugeProp:MultiStopRadialGradientBrushElement FocusScalesString="0, 0" CenterPointString="50, 50" RelativeBoundsMeasure="Percent" RelativeBounds="4, 4, 93, 93">
                                <ColorStops>
                                  <igGaugeProp:ColorStop Color="210, 210, 210" />
                                  <igGaugeProp:ColorStop Color="225, 225, 225" Stop="0.03989592" />
                                  <igGaugeProp:ColorStop Color="240, 240, 240" Stop="0.05030356" />
                                  <igGaugeProp:ColorStop Color="240, 240, 240" Stop="0.1006071" />
                                  <igGaugeProp:ColorStop Color="255, 255, 255" Stop="1" />
                                </ColorStops>
                              </igGaugeProp:MultiStopRadialGradientBrushElement>
                            </BrushElements>
                          </igGaugeProp:BrushElementGroup>
                        </BrushElements>
                        <StrokeElement>
                          <BrushElements>
                            <igGaugeProp:SolidFillBrushElement Color="Silver" />
                          </BrushElements>
                        </StrokeElement>
                      </Dial>
                      <Annotations>
                        <igGaugeProp:BoxAnnotation BoundsMeasure="Percent" Bounds="0, 15, 100, 100">
                        <Label FormatString="生产率" Font="黑体, 20px">
                        <BrushElements>
                        <igGaugeProp:SolidFillBrushElement Color="DarkSlateGray"/>
                        </BrushElements>
                        </Label>
                        </igGaugeProp:BoxAnnotation>
                        </Annotations>
                    </igGaugeProp:RadialGauge>
                    <igGaugeProp:SegmentedDigitalGauge Key="digital" DigitSpacing="2" Bounds="36, 75, 30, 10" BoundsMeasure="Percent" CornerExtent="10" Digits="4" Square="True" Text="0">
                        <FontBrushElements>
                            <igGaugeProp:SolidFillBrushElement Color="Black"/>
                        </FontBrushElements>
                        <BrushElements>
                            <igGaugeProp:SimpleGradientBrushElement EndColor="Gainsboro" StartColor="White" GradientStyle="BackwardDiagonal"/>
                        </BrushElements>
                        <StrokeElement>
                            <BrushElements>
                                <igGaugeProp:SolidFillBrushElement Color="150, 150, 150"/>
                            </BrushElements>
                        </StrokeElement>
                        <UnlitBrushElements>
                            <igGaugeProp:SolidFillBrushElement Color="Transparent"/>
                        </UnlitBrushElements>
                    </igGaugeProp:SegmentedDigitalGauge>
                  </Gauges>
                </iggauge:ultragauge>
	        </td>
	    </tr>
		
    </table>
</form>
</body>
</html>