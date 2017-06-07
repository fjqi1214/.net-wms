<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCColumnLineChartProcess.ascx.cs"
    Inherits="BenQGuru.Web.ReportCenter.UserControls.UCColumnLineChartProcess" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<igchart:UltraChart id="columnLineChart" runat="server" ChartType="ColumnLineChart"
    Version="9.1" Width="1060px" Height="450px">
    <border cornerradius="0" drawstyle="Solid" raised="False" color="Black" thickness="1"></border>

    <data datamember="" swaprowsandcolumns="False" useminmax="True" userowlabelscolumn="True"
        minvalue="-1.7976931348623157E+308" rowlabelscolumn="-1" zeroaligned="False"
        maxvalue="1.7976931348623157E+308">
                        <EmptyStyle Text="Empty" EnableLineStyle="False" ShowInLegend="False" EnablePE="False" EnablePoint="False">
                            <PointPE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PointPE>
                            <PointStyle CharacterFont="Microsoft Sans Serif, 7.8pt"></PointStyle>
                            <LineStyle MidPointAnchors="False" EndStyle="NoAnchor" DrawStyle="Dash" StartStyle="NoAnchor"></LineStyle>
                            <PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
                        </EmptyStyle>
                    </data>

    <colormodel colorbegin="DarkGoldenrod" colorend="Navy" alphalevel="150" modelstyle="CustomSkin"
        grayscale="False" scaling="None">
                        <Skin ApplyRowWise="False">
                            <PEs>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="108, 162, 36" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="148, 244, 17" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="7, 108, 176" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="53, 200, 255" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="230, 190, 2" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="255, 255, 81" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="0" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="215, 0, 5" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="254, 117, 16" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="252, 122, 10" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="255, 108, 66" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="108, 50, 36" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="108, 50, 36" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="27, 18, 176" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="27, 18, 176" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="230, 19, 20" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="230, 19, 20" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="0" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="215,100, 25" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="215,100, 25" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="25, 22, 10" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="25, 22, 10" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="18, 62, 36" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="18, 62, 36" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="75, 10, 176" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="75, 10, 176" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="230, 19, 2" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="230, 19, 2" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="0" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="215, 60, 115" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="215, 160, 215" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="25, 122, 105" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="25, 122, 105" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                
                                           <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="215,7, 125" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="215,7, 125" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="125, 122, 10" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="125, 122, 10" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="218, 2, 36" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="218, 2, 36" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="175, 109, 176" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="175, 109, 176" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="230, 198, 52" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="230, 198, 52" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="0" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="0, 160, 215" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="0, 160, 215" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                                <igchartprop:PaintElement FillGradientStyle="Horizontal" FillOpacity="255" FillStopOpacity="255" ElementType="Gradient" Fill="255, 255, 255" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="255, 255, 255" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></igchartprop:PaintElement>
                            </PEs>
                        </Skin>
                    </colormodel>
    <legend bordercolor="White" spanpercentage="20" visible="True"></legend>
    
    

    <columnlinechart>
                        <Column SeriesSpacing="1" ColumnSpacing="0" NullHandling="Zero">
                            <ChartText>
                                <igchartprop:ChartTextAppearance  ChartTextFont="Arial, 9pt" Column="-2" Row="-2" VerticalAlign="Far" 
                                    Visible="True" />
                            </ChartText>
                            
                        </Column>
                            <line>
                                <ChartText>
                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 9pt" Column="-2" Row="-2" VerticalAlign="Far" 
                                        Visible="True" />
                                </ChartText>
                            </line>
                        <ColumnData DataMember="" SwapRowsAndColumns="False" UseMinMax="False" UseRowLabelsColumn="False" MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308">
                            <EmptyStyle Text="Empty" EnableLineStyle="False" ShowInLegend="False" EnablePE="False" EnablePoint="False">
                                <PointPE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PointPE>
                                <PointStyle CharacterFont="Microsoft Sans Serif, 7.8pt"></PointStyle>
                                <LineStyle MidPointAnchors="False" EndStyle="NoAnchor" DrawStyle="Dash" StartStyle="NoAnchor"></LineStyle>
                                <PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
                            </EmptyStyle>
                        </ColumnData>
                        <LineData DataMember="" SwapRowsAndColumns="False" UseMinMax="False" UseRowLabelsColumn="False" MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308">
                            <EmptyStyle Text="Empty" EnableLineStyle="False" ShowInLegend="False" EnablePE="False" EnablePoint="False">
                                <PointPE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PointPE>
                                <PointStyle CharacterFont="Microsoft Sans Serif, 7.8pt"></PointStyle>
                                <LineStyle MidPointAnchors="False" EndStyle="NoAnchor" DrawStyle="Dash" StartStyle="NoAnchor"></LineStyle>
                                <PE FillGradientStyle="None" FillOpacity="255" FillStopOpacity="255" ElementType="SolidFill" Fill="Transparent" Hatch="None" Texture="LightGrain" ImageFitStyle="StretchedFit" FillStopColor="Transparent" StrokeOpacity="255" ImagePath="" Stroke="Black" StrokeWidth="1" ImageWrapMode="Tile" TextureApplication="Normal"></PE>
                            </EmptyStyle>
                        </LineData>
                    </columnlinechart>
    <axis>
            <pe elementtype="None" fill="Cornsilk" />
            <x linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" visible="True">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Center" 
                    itemformatstring="&lt;ITEM_LABEL&gt;" orientation="VerticalLeftFacing" 
                    verticalalign="Center" visible="False" OrientationAngle="0">
                    <serieslabels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Center" 
                        orientation="VerticalLeftFacing" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels> 
                                            <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                            <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
                            <TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>  
                
                            <Margin>
                                <Far MarginType="Percentage" Value="0"></Far>
                                <Near MarginType="Percentage" Value="0"></Near>
                            </Margin>            
            </x>
            <y linethickness="1" tickmarkinterval="10" tickmarkstyle="Smart" visible="True">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Center" 
                    itemformatstring="&lt;DATA_VALUE:00.##&gt;" orientation="Horizontal" 
                    verticalalign="Center">
                    <serieslabels font="Verdana, 7pt" fontcolor="DimGray" formatstring="" 
                        horizontalalign="Center" orientation="VerticalLeftFacing" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
                
                            <Margin>
                                <Far MarginType="Percentage" Value="0"></Far>
                                <Near MarginType="Percentage" Value="0"></Near>
                            </Margin>
                
            </y>
            <y2 linethickness="1" tickmarkinterval="10" tickmarkstyle="Smart" 
                visible="True">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Center" 
                    itemformatstring="&lt;DATA_VALUE:00.##&gt;" orientation="Horizontal" 
                    verticalalign="Center">
                    <serieslabels font="Verdana, 7pt" fontcolor="Gray" formatstring="" 
                        horizontalalign="Center" orientation="VerticalLeftFacing" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
                
                                            <Margin>
                                <Far MarginType="Percentage" Value="0"></Far>
                                <Near MarginType="Percentage" Value="0"></Near>
                            </Margin>
            </y2>
            <x2 linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" 
                visible="False">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Far" 
                    itemformatstring="&lt;ITEM_LABEL&gt;" orientation="VerticalLeftFacing" 
                    verticalalign="Center" visible="False">
                    <serieslabels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Center" 
                        orientation="Horizontal" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
            </x2>
            <z linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" visible="False">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Near" 
                    itemformatstring="" orientation="Horizontal" verticalalign="Center">
                    <serieslabels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Near" 
                        orientation="Horizontal" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
            </z>
            <z2 linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" 
                visible="False">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                    itemformatstring="" orientation="Horizontal" verticalalign="Center" 
                    visible="False">
                    <serieslabels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                        orientation="Horizontal" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
            </z2>
        </axis>
    <effects>
            <effects>
                <igchartprop:GradientEffect />
            </effects>
        </effects>
    <tooltips font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
        font-underline="False" />
</igchart:UltraChart>
