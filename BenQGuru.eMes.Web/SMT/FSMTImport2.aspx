<%@ Page language="c#" Codebehind="FSMTImport2.aspx.cs" AutoEventWireup="false" Inherits="BenQGuru.eMES.Web.SMT.FSMTImport2" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MPageSmtLoading</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=StyleSheet%>" rel=stylesheet>
		<script language="jscript">
			//复制的检查
			function CheckIfCopy()
			{
				if(event.keyCode==13)return false;
				var txtmocode = document.getElementById('txtMoQuery:_ctl0');
				if(txtmocode.value == '' || txtmocode.value == '0')
				{
					alert("请选择待维护工单");
					return false;
				}
				if(document.all.drpMOCopySourceQuery.value == '' || document.all.drpMOCopySourceQuery.value == '0')
				{
					alert("请选择来源工单");
					return false;
				}
			}
			function CheckImport()
			{
				var txtmocode = document.getElementById('txtMoQuery:_ctl0');
				if(txtmocode.value == '' || txtmocode.value == '0')
				{
					alert("请选择待维护工单");
					return false;
				}
				if( ''==document.all.fileExcel.value )
				{
					alert("请选择要导入的站表文件");
					return false;
				}
			}
			
			function CheckCompare()
			{
				if("comparesourcetype_excel" == document.all.hidComparesource.value)
				{
					if( ''==document.all.FileMOItem.value )
					{
						alert("请选择要比对的物料清单");
						return false;
					}
				}
				//物料清单根据mocode 和 opcode获取
				if( ''==document.all.fileExcel.value )
				{
					alert("请选择要比对的站表文件");
					return false;
				}
				SetImportBtnEnable();	//导入可用
				document.all.hidifImportUse.value = 'true';
			}
			
			function OnCheckBom()
			{
				if(document.all.CheckboxBOM.checked)
				{
					document.all.compareBomTR.style.display = 'block';
					document.all.compareSourceTR.style.display = 'block';
				}
				else
				{
					document.all.compareBomTR.style.display = 'none';
					document.all.compareSourceTR.style.display = 'none';
				}
				BOMCheckBoxChange();
				OnMOBOMSourceChange();
			}
			
			function OnMOBOMSourceChange()
			{
				if("comparesourcetype_db" == document.all.hidComparesource.value)
				{
					document.all.trDB.style.display = 'block';
					document.all.trExcel.style.display = 'none';
				}
				else if("comparesourcetype_excel" == document.all.hidComparesource.value)
				{
					document.all.trDB.style.display = 'none';
					document.all.trExcel.style.display = 'block';
				}
				else
				{
					document.all.trDB.style.display = 'block';
					document.all.trExcel.style.display = 'none';
				}
			}
		
			//比对BOM的CheckBox改变
			function BOMCheckBoxChange()
			{
				if(document.all.CheckboxBOM.checked == true)
				{
					SetBOMBtnEnable()		 //比对BOM可用
					if(document.all.hidifImportUse.value!='true')
					{SetImportBtnDisabled();  }//导入不可用
				}
				else
				{
					SetBOMBtnDisabled();	//比对BOM不可用
					SetImportBtnEnable();	//导入可用
				}
			}
			
			
			//比对BOM按钮控制
			//设置比对BOM可用
			function SetBOMBtnEnable()
			{
				document.all.cmdCompare.disabled = false;
				SetImportBtnEnable();
			}
			//设置比对BOM不可用
			function SetBOMBtnDisabled()
			{
				document.all.cmdCompare.disabled = true;
				SetImportBtnEnable();
			}
			
			//导入按钮控制
			//设置导入可用可用
			function SetImportBtnEnable()
			{
				document.all.cmdImport.disabled = false;
			}
			//设置导入不可用
			function SetImportBtnDisabled()
			{
				document.all.cmdImport.disabled = true;
			}
			
			
			//设置只读
			function SetReadOnly(thischeckbox)
			{
				var txtID = GetTxtID(thischeckbox);
				if(''!=txtID){
					document.getElementById(txtID).value ='';
					document.getElementById(txtID).readOnly = true;
				}
				
			}
			//取消只读
			function CancleReadOnly(thischeckbox)
			{
				var txtID = GetTxtID(thischeckbox);
				if(''!=txtID){document.getElementById(txtID).readOnly = false;}
			}
			
			function OnCheck(thischeckbox)
			{
				if(true == thischeckbox.checked){ 
					CancleReadOnly(thischeckbox);
				}
				else{
					SetReadOnly(thischeckbox);
				}
			}
			
			function GetTxtID(thischeckbox)
			{
				var returnTxtID="";
				switch(thischeckbox.id)
				{
					//case "chbFeederEdit":
					//	returnTxtID = "txtFeederEdit";
					//	break;
					case "chbSupplierItemEdit":
						returnTxtID = "txtSupplierItemEdit";
						break;
					case "chbLotNOEdit":
						returnTxtID = "txtLotNOEdit";
						break;
					case "chbSupplyCodeEdit":
						returnTxtID = "txtSupplyCodeEdit";
						break;
					case "chbDateCodeEdit":
						returnTxtID = "txtDateCodeEdit";
						break;
					case "chbPCBAEdit":
						returnTxtID = "txtPCBAEdit";
						break;
					case "chbBIOSEdit":
						returnTxtID = "txtBIOSEdit";
						break;
					case "chbVersionEdit":
						returnTxtID = "txtVersionEdit";
						break;
						
				}
				return returnTxtID;
			}
			
			function SetTreeChecked(ifchecked)
			{
				var tree = igtree_getTreeById('treeWebTree');
				var nodes = tree.getNodes();
				for(var i=0;i<nodes.length;i++)
				{
					nodes[i].setChecked(ifchecked);
				}
			}
			
			function OnchbExportByResCheck()
			{
				if(document.all.chbExportByRes.checked)
				{
					SetTreeChecked(false);
				}
				else
				{
					SetTreeChecked(true);
				}
			}
			
			function popCopyPage()
			{
				var RValue = window.showModalDialog("FSMTCopyDif.aspx","","scroll:0;status:0;help:0;resizable:1;dialogWidth:420px;dialogHeight:300px");
				if(RValue!= null && RValue=='true')
				{
					//此处刷新页面,调用隐藏控件的click事件,reflesh页面
					document.all.cmdFreshTree.click();
				}
				return false;
			}
			
			function popDifferentBomPage()
			{
				var RValue = window.showModalDialog("FSMTBOMCompareDif.aspx","","scroll:0;status:0;help:0;resizable:1;dialogWidth:1024px;dialogHeight:768px");
				//document.all.cmdFreshTree.click();
				return false;
			}
			function popErrorPage()
			{
				window.showModalDialog("FSMTImportError.aspx","","scroll:0;status:0;help:0;resizable:1;dialogWidth:800px;dialogHeight:600px");
				return false;
			}
			/*
			function popSelectrPage()
			{
				//var returnMOValue = window.showModalDialog("FSMTSelectMO.aspx","","scroll:0;status:0;help:0;resizable:1;dialogWidth:900px;dialogHeight:600px");
				var returnMOValue = window.showModalDialog("FSMTSelectMO.aspx?mocode="+escape(document.all.txtMOCode.value),"","scroll:0;status:0;help:0;resizable:1;dialogWidth:900px;dialogHeight:600px");
				if(returnMOValue!=null && returnMOValue!='')
				{
					document.all.txtMOCode.value = returnMOValue;
					document.all.hidtxtMOCode.value = returnMOValue;
					//刷新页面
					document.all.cmdchangeMO.click();
				}
				return false;
			}
			*/
			function L_popSelectrPage()
			{
				//刷新页面
				document.all.cmdchangeMO.click();
				return false;
			
			}
			
			function Init()
			{
				OnCheckBom();
				var txtmocode = document.getElementById('txtMoQuery:_ctl0');
				txtmocode.focus();
			}
			function OnKeyPress()
			{
				if('txtMoQuery:_ctl0'== event.srcElement.name  && 13 == window.event.keyCode )
				{
					window.event.keyCode  = 0;
					var btnmo = document.getElementById('txtMoQuery:_ctl2');
					btnmo.click();
					L_popSelectrPage();
					
				}
				if(13 == window.event.keyCode){window.event.keyCode  = 0;}
				
			}
			
			
			
		</script>
	</HEAD>
	<body onkeypress="OnKeyPress()" bottomMargin="0" leftMargin="0" topMargin="0" scroll="yes"
		onload="Init()" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 100%; POSITION: absolute; TOP: 0px; HEIGHT: 100%"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<TBODY>
					<TR>
						<TD style="PADDING-LEFT: 8px; HEIGHT: 20px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">SMT上料防呆维护</asp:label><asp:textbox id="txtOperationCode" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtResourceCode" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtItemCodeQuery" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtRouteCode" runat="server" Visible="False"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="PADDING-LEFT: 8px; HEIGHT: 20px">
							<TABLE class="query" id="Table13" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TBODY>
									<tr>
						</TD>
						<td colSpan="8"></td>
					</TR>
					<TR>
						<td class="fieldNameLeft" noWrap><asp:label id="lblMoQuery" runat="server">工单代码</asp:label></td>
						<td class="fieldValue"><cc2:selectabletextbox id="txtMoQuery" runat="server" Width="120px" Type="mo"></cc2:selectabletextbox></td>
						<TD style="DISPLAY: none" noWrap><FONT face="宋体"><asp:dropdownlist id="drpMOCode" runat="server" Width="120px" AutoPostBack="True"></asp:dropdownlist></FONT></TD>
						<TD class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></TD>
						<TD noWrap><asp:textbox id="txtItemCode" runat="server" CssClass="textbox" Width="120px" ReadOnly="True"></asp:textbox></TD>
						<TD class="fieldName" noWrap><FONT face="宋体"><asp:label id="lblMOCopySourceQuery" runat="server"> 来源工单</asp:label></FONT></TD>
						<TD noWrap><FONT face="宋体"><asp:dropdownlist id="drpMOCopySourceQuery" runat="server" Width="130px" AutoPostBack="True"></asp:dropdownlist></FONT></TD>
						<TD class="fieldName" noWrap><FONT face="宋体"><asp:label id="Label2" runat="server">选择机台</asp:label></FONT></TD>
						<TD noWrap><FONT face="宋体"><asp:dropdownlist id="drpResource" runat="server" Width="130px"></asp:dropdownlist></FONT></TD>
						<TD class="fieldName" noWrap><FONT face="宋体"><INPUT class="submitImgButton" id="cmdCopy" type="submit" value="复 制" name="cmdCopy" runat="server"></FONT></TD>
					</TR>
				</TBODY>
			</TABLE>
			</TD></TR>
			<TR>
				<TD style="PADDING-LEFT: 8px; HEIGHT: 20px">
					<TABLE class="query" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="fieldValue">&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="CheckboxBOM" onclick="OnCheckBom()" type="checkbox" CHECKED name="CheckboxBOM"
									runat="server">比对BOM</TD>
							<td></td>
						</TR>
						<TR>
							<TD class="fieldName" noWrap><asp:label id="Label5" runat="server"> 选择站表导入文件</asp:label></TD>
							<TD class="fieldValue"><INPUT class="textStyle" id="fileExcel" type="file" size="100" name="fileExcel" runat="server"></TD>
							<TD class="fieldName"><FONT face="宋体"><INPUT class="submitImgButton" id="cmdImport" type="submit" value="导 入" name="cmdImport"
										runat="server"></FONT></TD>
						</TR>
						<tr id="compareSourceTR">
							<td class="fieldName"><asp:label id="lblVisibleStyle" runat="server">比对工单BOM来源</asp:label></td>
							<td colSpan="2"><asp:radiobuttonlist id="rblMOBOMSourceSelect" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"></asp:radiobuttonlist></td>
						</tr>
						<TR id="compareBomTR">
							<td colSpan="2">
								<table>
									<tr id="trDB">
										<TD class="fieldName" noWrap><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="lblSapOpcode" runat="server">SAP工序代码</asp:label></FONT></TD>
										<TD noWrap><asp:textbox id="txtSapOPCode" runat="server" CssClass="textbox" Width="120px"></asp:textbox></TD>
									</tr>
									<tr id="trExcel">
										<TD class="fieldName" noWrap><FONT face="宋体">&nbsp;&nbsp; </FONT>
											<asp:label id="Label7" runat="server">工单物料清单文件</asp:label></TD>
										<TD class="fieldValue"><INPUT class="textStyle" id="FileMOItem" type="file" size="100" name="fileExcel" runat="server"></TD>
									</tr>
								</table>
							</td>
							<TD class="fieldName"><INPUT class="submitImgButton" id="cmdCompare" type="submit" value="比对BOM" name="cmdCompare"
									runat="server"></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD>
					<TABLE id="Table2" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; WIDTH: 100%; PADDING-TOP: 4px; HEIGHT: 100%"
						cellSpacing="0" cellPadding="0" width="300" border="0">
						<TBODY>
							<TR>
								<TD style="PADDING-LEFT: 8px; WIDTH: 200px" vAlign="top" align="left">
									<TABLE class="fieldGrid" id="Table3" style="HEIGHT: 100%" cellSpacing="0" cellPadding="0"
										width="200" border="0">
										<TR>
											<TD vAlign="top" align="left">
												<TABLE class="edit" id="Table9" height="100" cellSpacing="0" cellPadding="0" width="100%"
													border="0">
													<tr>
														<td class="fieldNameRight" style="HEIGHT: 20px"><FONT face="宋体">&nbsp; </FONT>
															<asp:label id="Label3" runat="server">机台资料</asp:label></td>
													</tr>
													<TR>
														<TD style="PADDING-RIGHT: 8px" vAlign="top"><ignav:ultrawebtree id="treeWebTree" runat="server" Width="100%" Font-Size="12px" Height="100%" Cursor="hand"
																WebTreeTarget="ClassicTree" ImageDirectory="/ig_Images2/" JavaScriptFilename="/ig_scripts2/ig_webtree2.js" DisabledClass="DisabledClass"
																CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif" Indentation="20" HiliteClass="HiliteClass">
																<SelectedNodeStyle Cursor="Hand" ForeColor="White" BackColor="DarkBlue"></SelectedNodeStyle>
																<DisabledStyle ForeColor="LightGray"></DisabledStyle>
																<Levels>
																	<ignav:Level Index="0"></ignav:Level>
																	<ignav:Level Index="1"></ignav:Level>
																	<ignav:Level Index="2"></ignav:Level>
																	<ignav:Level Index="3"></ignav:Level>
																	<ignav:Level Index="4"></ignav:Level>
																	<ignav:Level Index="5"></ignav:Level>
																	<ignav:Level Index="6"></ignav:Level>
																</Levels>
																<Styles>
																	<ignav:Style Cursor="Hand" ForeColor="White" BackColor="DarkBlue" CssClass="HiliteClass"></ignav:Style>
																	<ignav:Style ForeColor="LightGray" CssClass="DisabledClass"></ignav:Style>
																</Styles>
															</ignav:ultrawebtree></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
								<TD>
									<TABLE id="Table5" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0" width="300"
										border="0">
										<TBODY>
											<TR>
												<TD class="fieldGrid" vAlign="top" align="center" height="100%"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
														<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
															Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
															BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
															Name="gridWebGrid" TableLayout="Fixed">
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
													</igtbl:ultrawebgrid></TD>
											</TR>
											<TR height="20">
												<TD>
													<table id="Table11" height="100%" cellPadding="0" width="100%" border="0">
														<tr>
															<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
															<td style="DISPLAY: none"><asp:checkbox id="chbifImportCheck" runat="server" Text="" Checked></asp:checkbox></td>
															<TD class="smallImgButton" style="DISPLAY: none"><INPUT id="cmdReFlesh" type="submit" value="  " name="cmdReFlesh" runat="server">
																|</TD>
															<TD class="smallImgButton" style="DISPLAY: none"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
																	runat="server"> |</TD>
															<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
																|
															</TD>
															<TD noWrap><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
															<td noWrap align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
														</tr>
													</table>
												</TD>
											</TR>
											<TR>
												<TD vAlign="top">
													<TABLE class="query" id="Table7" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0"
														width="300" border="0">
														<TR>
															<TD vAlign="top">
																<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD class="fieldNameLeft" style="HEIGHT: 36px"><asp:label id="lblStationEdit" runat="server"> 站位</asp:label></TD>
																		<TD></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtStationEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly DESIGNTIMEDRAGDROP="1285"></asp:textbox></TD>
																		<TD class="fieldName" style="HEIGHT: 36px"><asp:label id="lblItemCodeEdit" runat="server">料号</asp:label></TD>
																		<TD></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px" noWrap><asp:textbox id="txtItemCodeEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="fieldNameLeft" style="HEIGHT: 36px"><asp:label id="lblFeederEdit" runat="server" DESIGNTIMEDRAGDROP="975"> 料架规格代码</asp:label></TD>
																		<TD style="WIDTH: 30px"><INPUT id="chbFeederEdit" style="DISPLAY: none" type="checkbox" name="chbFeederEdit" runat="server"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtFeederEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly DESIGNTIMEDRAGDROP="1285"></asp:textbox></TD>
																		<TD class="fieldName" style="HEIGHT: 36px"><asp:label id="lblSupplierItemEdit" runat="server"> 厂商料号</asp:label></TD>
																		<TD style="WIDTH: 30px"><INPUT id="chbSupplierItemEdit" onclick="OnCheck(this)" type="checkbox" name="chbSupplierItemEdit"
																				runat="server"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtSupplierItemEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="fieldNameLeft"><asp:label id="lblLotNOEdit" runat="server" DESIGNTIMEDRAGDROP="1282"> 生产批次</asp:label></TD>
																		<TD><INPUT id="chbLotNOEdit" onclick="OnCheck(this)" type="checkbox" name="chbLotNOEdit" runat="server"
																				DESIGNTIMEDRAGDROP="1284"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtLotNOEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly DESIGNTIMEDRAGDROP="1285"></asp:textbox></TD>
																		<TD class="fieldName"><asp:label id="lblSupplyCodeEdit" runat="server" DESIGNTIMEDRAGDROP="1440"> 厂商</asp:label></TD>
																		<TD><INPUT id="chbSupplyCodeEdit" onclick="OnCheck(this)" type="checkbox" name="chbSupplyCodeEdit"
																				runat="server"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtSupplyCodeEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="fieldNameLeft"><asp:label id="lblDateCodeEdit" runat="server" DESIGNTIMEDRAGDROP="1912"> 生产日期</asp:label></TD>
																		<TD><INPUT id="chbDateCodeEdit" onclick="OnCheck(this)" type="checkbox" name="chbDateCodeEdit"
																				runat="server" DESIGNTIMEDRAGDROP="1914"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtDateCodeEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly DESIGNTIMEDRAGDROP="1915"></asp:textbox></TD>
																		<TD class="fieldName"><asp:label id="lblPCBAEdit" runat="server" DESIGNTIMEDRAGDROP="1917"> PCBA版本</asp:label></TD>
																		<TD><INPUT id="chbPCBAEdit" onclick="OnCheck(this)" type="checkbox" name="chbPCBAEdit" runat="server"
																				DESIGNTIMEDRAGDROP="1919"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtPCBAEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly DESIGNTIMEDRAGDROP="1920"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="fieldNameLeft"><asp:label id="lblBIOSEdit" runat="server"> BIOS版本</asp:label></TD>
																		<TD><INPUT id="chbBIOSEdit" onclick="OnCheck(this)" type="checkbox" name="chbBIOSEdit" runat="server"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtBIOSEdit" runat="server" CssClass="textbox" Width="150px" ReadOnly></asp:textbox></TD>
																		<TD class="fieldName"><asp:label id="lblVersionEdit" runat="server"> 物料版本</asp:label></TD>
																		<TD><INPUT id="chbVersionEdit" onclick="OnCheck(this)" readOnly type="checkbox" name="Checkbox8"
																				runat="server"></TD>
																		<TD class="fieldValue" style="HEIGHT: 36px"><asp:textbox id="txtVersionEdit" runat="server" CssClass="textbox" Width="150px"></asp:textbox></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD id="Table8" align="top">
													<TABLE class="toolBar" id="Table12" border="0">
														<TR>
															<TD style="DISPLAY: none" noWrap width="100"><nobr><INPUT id="chbExportByRes" onclick="OnchbExportByResCheck()" type="checkbox" name="chbExportByRes"
																		runat="server" DESIGNTIMEDRAGDROP="1919"><asp:label id="Label1" runat="server">指定机台导出</asp:label>&nbsp;</nobr>
															</TD>
															<TD noWrap width="100"><nobr><INPUT id="chbIfContainFeeder" type="checkbox" CHECKED name="chbIfContainFeeder" runat="server"
																		DESIGNTIMEDRAGDROP="1919"><asp:label id="Label4" runat="server">导出包含Feeder</asp:label>&nbsp;</nobr>
															</TD>
															<td class="toolBar"><INPUT class="submitImgButton" id="cmdMOClose" type="submit" value="导 出" name="cmdMOClose"
																	runat="server"></td>
															<TD class="toolBar" align="center"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></TD>
															<TD class="toolBar" align="center"><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
																	runat="server"></TD>
															<TD class="toolBar" style="DISPLAY: none"><INPUT class="submitImgButton" id="cmdRelease" type="submit" value="删 除" name="cmdRelease"
																	runat="server"></TD>
															<TD class="toolBar"><FONT face="宋体"></FONT></TD>
															<td class="toolBar" style="DISPLAY: none"><INPUT id="hidifImportUse" type="hidden" value="false" name="hidifImportUse" runat="server"></td>
															<td class="toolBar" style="DISPLAY: none"><INPUT class="submitImgButton" id="cmdchangeMO" type="submit" value="工单选择改变" name="cmdchangeMO"
																	runat="server"></td>
															<td class="toolBar" style="DISPLAY: none"><INPUT class="submitImgButton" id="cmdFreshTree" type="submit" value="刷新树结构" name="cmdFreshTree"
																	runat="server"></td>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TBODY>
									</TABLE>
								</TD>
							</TR>
						</TBODY>
					</TABLE>
				</TD>
			</TR>
			</TBODY></TABLE></TD></TR></TBODY></TABLE><input id="hidComparesource" type="hidden" runat="server"><input id="hidtxtMOCode" type="hidden" name="hidtxtMOCode" runat="server">
		</form>
	</body>
</HTML>
