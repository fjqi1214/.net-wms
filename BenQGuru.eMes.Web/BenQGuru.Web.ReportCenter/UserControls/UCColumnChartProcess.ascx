<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCColumnChartProcess.ascx.cs"
    Inherits="BenQGuru.Web.ReportCenter.UserControls.UCColumnChartProcess" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<igchart:UltraChart ID="columnChart" runat="server" Width="1060px" Height="450px" EmptyChartText=""
    Version="9.1" ChartType="ColumnChart"  >  
         <ColumnChart>
        <ChartText>
            <igchartprop:ChartTextAppearance ChartTextFont="Arial, 9pt" Column="-2" Row="-2" VerticalAlign="Far" 
                Visible="True" />
        </ChartText>
    </ColumnChart>
   <Border CornerRadius="0" DrawStyle="Solid" Raised="False" Color="Black" Thickness="1"></Border>

     <legend bordercolor="White"  spanpercentage="20" visible="True"></legend>
 <colormodel alphalevel="255" colorbegin="Pink" colorend="DarkRed" 
            modelstyle="CustomLinear">
        </colormodel>
        <compositechart>            
            <chartlayers>
                <igchartprop:ChartLayerAppearance ChartType="BarChart" Key="chartLayer1">
                    <charttypeappearances>
                        <igchartprop:BarChartAppearance>
                        </igchartprop:BarChartAppearance>
                    </charttypeappearances>
                </igchartprop:ChartLayerAppearance>
            </chartlayers>
            <chartareas>
                <igchartprop:ChartArea Key="area1">
                    <axes>
                        <igchartprop:AxisItem DataType="Numeric" Key="axis1" OrientationType="X_Axis" 
                            SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                            <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                thickness="1" visible="True" />
                            <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                thickness="1" visible="False" />
                            <labels horizontalalign="Near" itemformatstring="" orientation="Horizontal" 
                                verticalalign="Center">
                                <serieslabels horizontalalign="Center" orientation="Horizontal" 
                                    verticalalign="Center">
                                </serieslabels>
                            </labels>
                        </igchartprop:AxisItem>
                        <igchartprop:AxisItem DataType="String" Key="axis2" OrientationType="Y_Axis" 
                            SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                            <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                thickness="1" visible="True" />
                            <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                thickness="1" visible="False" />
                            <labels horizontalalign="Near" itemformatstring="" orientation="Horizontal" 
                                verticalalign="Center">
                                <serieslabels horizontalalign="Center" orientation="Horizontal" 
                                    verticalalign="Center">
                                </serieslabels>
                            </labels>
                        </igchartprop:AxisItem>
                    </axes>
                    <gridpe elementtype="None" />
                </igchartprop:ChartArea>
            </chartareas>
           
        </compositechart>

    <StackChart StackStyle="Normal"></StackChart>
       <axis>
            <pe elementtype="None" fill="Cornsilk" />
            <x linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" visible="True">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Near" 
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
            </x>
            <y linethickness="1" tickmarkinterval="10" tickmarkstyle="Smart" visible="True">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Far" 
                    itemformatstring="&lt;DATA_VALUE:00.##&gt;" orientation="Horizontal" 
                    verticalalign="Center">
                    <serieslabels font="Verdana, 7pt" fontcolor="DimGray" formatstring="" 
                        horizontalalign="Far" orientation="VerticalLeftFacing" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
            </y>
            <y2 linethickness="1" tickmarkinterval="10" tickmarkstyle="Smart" 
                visible="False">
                <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                    thickness="1" visible="True" />
                <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                    thickness="1" visible="False" />
                <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                    itemformatstring="&lt;DATA_VALUE:00.##&gt;" orientation="Horizontal" 
                    verticalalign="Center" visible="False">
                    <serieslabels font="Verdana, 7pt" fontcolor="Gray" formatstring="" 
                        horizontalalign="Near" orientation="VerticalLeftFacing" verticalalign="Center">
                        <layout behavior="Auto">
                        </layout>
                    </serieslabels>
                    <layout behavior="Auto">
                    </layout>
                </labels>
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
        <tooltips font-bold="False" font-italic="False" font-overline="False" 
            font-strikeout="False" font-underline="False"  />  
</igchart:UltraChart>
