<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCParetoChartProcess.ascx.cs" 
Inherits="BenQGuru.eMES.Web.WebQuery.UserControls.UCParetoChartProcess" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<igchart:UltraChart ID="paretoChart" runat="server" 
        ChartType="ParetoChart" Version="9.1" Height="450px" 
        Width="1060px" 
        EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource">
        <ColorModel AlphaLevel="150" ColorBegin="LimeGreen" ColorEnd="Red" 
            ModelStyle="CustomSkin" Grayscale="False" Scaling="None">
        </ColorModel>
        <legend bordercolor="White" spanpercentage="25" visible="True"></legend>
        
        <ParetoChart ColumnSpacing="0.5" NullHandling="Zero" LineLabel="Running Total" ShowLineInLegend="True">
                        <LinePE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Blue" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></LinePE>
                        <LineStyle MidPointAnchors="True" EndStyle="RoundAnchor" DrawStyle="Solid" StartStyle="RoundAnchor"></LineStyle>
                    </ParetoChart>

        <Data DataMember="" SwapRowsAndColumns="False" UseMinMax="False" UseRowLabelsColumn="False" MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308">
                        <EmptyStyle Text="Empty" EnableLineStyle="False" ShowInLegend="False" EnablePE="False" EnablePoint="False">
                            <PointPE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PointPE>
                            <PointStyle CharacterFont="Microsoft Sans Serif, 7.8pt"></PointStyle>
                            <LineStyle MidPointAnchors="False" EndStyle="NoAnchor" DrawStyle="Dash" StartStyle="NoAnchor"></LineStyle>
                            <PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
                        </EmptyStyle>
        </Data>
        
        <Axis>
            <PE ElementType="None" Fill="Cornsilk" />
            <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                    ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                    VerticalAlign="Center">
                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                        HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                        <Layout Behavior="Auto">
                        </Layout>
                    </SeriesLabels>
                    <Layout Behavior="Auto">
                    </Layout>
                </Labels>
            </X>
            <Y Extent="40" LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" 
                Visible="True">
                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" HorizontalAlign="Far" 
                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                    VerticalAlign="Center">
                    <SeriesLabels FormatString="" HorizontalAlign="Far" Orientation="Horizontal" 
                        VerticalAlign="Center">
                    </SeriesLabels>
                </Labels>
            </Y>
            
            <Y2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black" RangeType="Automatic" TickmarkInterval="0" LineThickness="1" Extent="80" LogBase="10" RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">

                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                    ItemFormatString="&lt;DATA_VALUE:##.##&gt;" Orientation="Horizontal" 
                    VerticalAlign="Center">
                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                        HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                        <Layout Behavior="Auto">
                        </Layout>
                    </SeriesLabels>
                    <Layout Behavior="Auto">
                    </Layout>
                </Labels>
            </Y2>
            <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                Visible="False">
                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                    ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                    VerticalAlign="Center">
                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                        HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                        <Layout Behavior="Auto">
                        </Layout>
                    </SeriesLabels>
                    <Layout Behavior="Auto">
                    </Layout>
                </Labels>
            </X2>
            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                    ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                        Orientation="Horizontal" VerticalAlign="Center">
                        <Layout Behavior="Auto">
                        </Layout>
                    </SeriesLabels>
                    <Layout Behavior="Auto">
                    </Layout>
                </Labels>
            </Z>
            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                Visible="False">
                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                    Thickness="1" Visible="True" />
                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                    Thickness="1" Visible="False" />
                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                    ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                        Orientation="Horizontal" VerticalAlign="Center">
                        <Layout Behavior="Auto">
                        </Layout>
                    </SeriesLabels>
                    <Layout Behavior="Auto">
                    </Layout>
                </Labels>
            </Z2>
        </Axis>
        <ParetoChart>
            <LineStyle EndStyle="RoundAnchor" MidPointAnchors="True" 
                StartStyle="RoundAnchor" />
            <LinePE Fill="Blue" />
        </ParetoChart>

    <effects>
        <Effects>
            <igchartprop:GradientEffect>
            </igchartprop:GradientEffect>
        </Effects>
    </effects>
    <tooltips font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
        font-underline="False" />
</igchart:UltraChart>