<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FBasicDataImp.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.ImportData.FBasicDataImp" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport"
    Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns:igtbl>
<head>
    <title>FBasicDataImp</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">

    <script language="javascript">
		function Check()
		{
			if( document.all.DownLoadPathBom.value == "" )
			{
				alert("上传文件不能为空");
				return false;
			}
		}
		//下载导入模板
		function DownLoadFile()
		{ 
			//debugger;
			try
			{
				var hf = document.all.aFileDownLoad.href; 
				if (hf=="") return;
				var path;
				var pix = hf.substr(0, 4);
				if (pix.toLowerCase()=="file")  path = hf.substr(8, hf.length-8);
				else if (pix.toLowerCase()=="http") path = hf;
				var fl = path.split('/');
				var file = fl[fl.length-2]+'/'+fl[fl.length-1];		
				var strDownUrl="http://"+fl[fl.length-4]+"/"+fl[fl.length-3]+"/FDownload.aspx?&fileName="+escape(file);
				window.open(strDownUrl,"ExportWindow","top=60000,left=60000,height=1,width=1,status=no,toolbar=no,menubar=no,location=no");
				
				return false;
		
			}catch(e) { alert(e.description); }			    
		}
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="FORM1" method="post" runat="server">
        <table id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px"
            height="100%" width="100%">
            <tbody>
                <tr class="moduleTitle">
                    <td>
                        <asp:Label ID="lblBasicDataIn" runat="server" CssClass="labeltopic">基础资料导入</asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <table class="query" id="Table2" height="100%" width="100%">
                            <tr align="right">
                                <td class="fieldNameLeft" nowrap height="20">
                                    <asp:Label ID="lblImportType" Visible="false" runat="server"> 导入类型</asp:Label></td>
                                <td style="width: 447px">
                                    <asp:DropDownList ID="InputTypeDDL" AutoPostBack="True" Width="0px" runat="server"
                                        OnSelectedIndexChanged="InputTypeDDL_SelectedIndexChanged">
                                        <asp:ListItem Value="Item">产品</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td class="fieldNameLeft" nowrap height="20">
                                    <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label></td>
                                <td style="width: 300px">
                                    <input id="DownLoadPathBom" style="width: 454px; height: 22px" type="file" size="56"
                                        name="File1" runat="server"></td>
                                <td class="fieldNameLeft" nowrap height="20">
                                    <asp:Label ID="lblInTemplet" runat="server"> 导入模板：</asp:Label></td>
                                <td nowrap>
                                    <a id="aFileDownLoad" style="display: none; color: blue" href="" target="_blank"
                                        runat="server">
                                        <asp:Label ID="lblDown" runat="server">下载</asp:Label></a> <span style="cursor: hand;
                                            color: blue; text-decoration: underline" onclick="DownLoadFile()">
                                            <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                                </td>
                                <td nowrap width="100%">
                                </td>
                                <td class="toolBar">
                                    <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd"
                                        runat="server" onserverclick="cmdAdd_ServerClick"></td>
                                <td>
                                    <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                        runat="server" onserverclick="cmdReturn_ServerClick"></td>
                                <td align="center">
                                    <input class="submitImgButton" id="cmdView" onmouseover="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search_0.gif&quot;)';"
                                        disabled onmouseout="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search.gif&quot;)';"
                                        type="submit" value="察 看" name="cmdQuery" visible="false" runat="server" onserverclick="cmdQuery_ServerClick">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td colspan="8">
                                    <div id="DIVForDownload" style="width: 1px; position: relative; height: 1px" runat="server"
                                        ms_positioning="GridLayout">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr height="100%">
                    <td class="fieldGrid">
                        <displaylayout tablelayout="Fixed" name="webGrid" rowselectorsdefault="No" cellpaddingdefault="4"
                            allowcolsizingdefault="Free" bordercollapsedefault="Separate" headerclickactiondefault="SortSingle"
                            selecttypecelldefault="Single" selecttyperowdefault="Single" version="2.00" rowheightdefault="20px"
                            allowsortingdefault="Yes" stationarymargins="Header" colwidthdefault=""><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
									<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
										Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
										BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
										Name="gridWebGrid" TableLayout="Fixed" NoDataMessage="">
										<AddNewBox>
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</AddNewBox>
										<Pager StyleMode="CustomLabels" AllowPaging="false">
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</Pager>
										<HeaderStyleDefault BorderWidth="1px" Font-Size="12px" Font-Bold="True" BorderColor="White" BorderStyle="Dashed"
											HorizontalAlign="Left" BackColor="#ABABAB">
											<BorderDetails ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
												ColorLeft="White"></BorderDetails>
										</HeaderStyleDefault>
										<RowSelectorStyleDefault BackColor="#EBEBEB"></RowSelectorStyleDefault>
										<FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana" BorderColor="#ABABAB"
											BorderStyle="Groove" Height="100%"></FrameStyle>
										<FooterStyleDefault BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ActivationObject BorderStyle="Dotted"></ActivationObject>
										<EditCellStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="Black" BorderStyle="None">
											<Padding Bottom="1px"></Padding>
										</EditCellStyleDefault>
										<RowAlternateStyleDefault BackColor="White"></RowAlternateStyleDefault>
										<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="#D7D8D9" BorderStyle="Solid"
											HorizontalAlign="Left">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand></igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid>
								<ADDNEWBOX>
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
								<IMAGEURLS ImageDirectory="/ig_grid2_Images/"></IMAGEURLS>
							</displaylayout>
                        <bands><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
							</bands>
                    </td>
                </tr>
                <tr class="normal">
                    <td>
                        <table id="Table3" height="100%" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td nowrap>
                                        <asp:CheckBox ID="chbSelectAll" runat="server" Visible="false" AutoPostBack="True"
                                            Width="124px" Text="全选" Enabled="False"></asp:CheckBox></td>
                                    <td nowrap align="right">
                                        <span>
                                            <asp:Label ID="lblContainNG" runat="server" Visible="false">容错选项：</asp:Label></span></td>
                                    <td nowrap align="left">
                                        <asp:RadioButtonList ID="ImportList" Visible="false" runat="server" RepeatColumns="5">
                                            <asp:ListItem Selected="True" Value="Skip">跳过</asp:ListItem>
                                            <asp:ListItem Value="RoolBack">回滚</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td style="font-weight: bold; left: 90%; position: absolute" valign="middle">
                                        <span>
                                            <asp:Label ID="lblAll" runat="server" Visible="false">共</asp:Label></span>
                                        <asp:Label ID="lblCount" runat="server" Visible="false"> 0 </asp:Label><span><asp:Label
                                            ID="lblNum" runat="server" Visible="false">笔</asp:Label></span></td>
                                    <tr class="normal">
                                    </tr>
                                    <tr class="toolBar">
                                        <td colspan="4">
                                            <table class="toolBar" id="Table4">
                                                <tr>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                            </tbody>
                        </table>
    </form>
    </TD></TR></TBODY></TABLE>
</body>
</html>
