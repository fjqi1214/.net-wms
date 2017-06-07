<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCColumnChartProcess.ascx.cs"
    Inherits="BenQGuru.eMES.Web.WebQuery.UserControls.UCColumnChartProcess" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" 
namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>
<igchart:UltraChart ID="columnChart" runat="server" Width="1060px" Height="450px"
    Version="9.1">
    <Legend BorderColor="White" SpanPercentage="20" Visible="True"></Legend>
    <ColumnChart>
        <ChartText>
            <igchartprop:ChartTextAppearance ChartTextFont="Arial, 9pt" Column="-2" Row="-2" VerticalAlign="Far" 
                Visible="True" />
        </ChartText>
    </ColumnChart>
     <Border CornerRadius="0" DrawStyle="Solid" Raised="False" Color="Black" Thickness="1"></Border>
   <Data DataMember="" SwapRowsAndColumns="False" UseMinMax="true" UseRowLabelsColumn="true" MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308">
                                <EmptyStyle Text="Empty" EnableLineStyle="False" ShowInLegend="False" EnablePE="False" EnablePoint="False">
                                    <PointPE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PointPE>
                                    <PointStyle CharacterFont="Microsoft Sans Serif, 7.8pt"></PointStyle>
                                    <LineStyle MidPointAnchors="False" EndStyle="NoAnchor" DrawStyle="Dash" StartStyle="NoAnchor"></LineStyle>
                                    <PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
                                </EmptyStyle>
                            </Data>

  <ColorModel ColorBegin="LimeGreen" ColorEnd="Red" AlphaLevel="150" ModelStyle="CustomSkin" Grayscale="False" Scaling="None">
    </ColorModel>

    
    <Axis>
        <PE ElementType="None" Fill="Red" FillStopColor="Red" Stroke="White" Texture="FabricB" />
        <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
            <MajorGridLines AlphaLevel="255" Color="Black" DrawStyle="Dot" Thickness="1" Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="Black" DrawStyle="Dot" Thickness="1" Visible="False" />
            <Labels Font="Verdana, 7pt" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                Orientation="VerticalLeftFacing" VerticalAlign="Center" FontColor="Black">
                <SeriesLabels Font="Verdana, 7pt" FormatString="" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                    VerticalAlign="Center">
                    <Layout Behavior="Auto">
                    </Layout>
                </SeriesLabels>
                <Layout Behavior="Auto">
                </Layout>
            </Labels>
        </X>
        <Y Extent="60" LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" 
            Visible="True">
            <MajorGridLines AlphaLevel="255" Color="Black" DrawStyle="Dot" Thickness="1" Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="Black" DrawStyle="Dot" Thickness="1" Visible="False" />
            <Labels Font="Verdana, 7pt" HorizontalAlign="Far"
                Orientation="Horizontal" VerticalAlign="Center" FontColor="Black">
                <SeriesLabels Font="Verdana, 7pt" FormatString="" HorizontalAlign="Far" Orientation="Horizontal"
                    VerticalAlign="Center">
                    <Layout Behavior="Auto">
                    </Layout>
                </SeriesLabels>
                <Layout Behavior="Auto">
                </Layout>
            </Labels>
        </Y>
        <Y2 Visible="False" TickmarkInterval="0">
            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                Visible="False" />
            <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" VerticalAlign="Center">
                </SeriesLabels>
            </Labels>
        </Y2>
        <X2 TickmarkInterval="0" Visible="False">
            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                Visible="False" />
            <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" VerticalAlign="Center">
                </SeriesLabels>
            </Labels>
        </X2>
        <Z LineColor="Blue" TickmarkInterval="0" Visible="False">
            <MajorGridLines AlphaLevel="255" Color="White" DrawStyle="Dot" Thickness="1" Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                Visible="False" />
            <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" VerticalAlign="Center">
                </SeriesLabels>
            </Labels>
        </Z>
        <Z2 TickmarkInterval="0" Visible="False">
            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                Visible="True" />
            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                Visible="False" />
            <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" VerticalAlign="Center">
                </SeriesLabels>
            </Labels>
        </Z2>
    </Axis>
    <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" />
</igchart:UltraChart>
