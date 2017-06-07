<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="FMaterialQuery.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialQuery" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>物料交易查询</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		
	           

		<script language="javascript">
		
		    function InitPageWhenLoad()
            {
//            txtPOEdit_TextChanged();

//            document.getElementById("cmdSave").disabled=false;
//            document.getElementById("cmdAdd").disabled=false;
            }

		
			function OpenSelectTypeWindow()
			{
				var str = document.getElementById("txtTransTypeQuery").value;
				var result = window.showModalDialog("FQueryItemSelTransTypeSP.aspx?selecteditem="+str, "" ,"dialogWidth:400px;dialogHeight:220px;scroll:none;help:no;status:no") ;
				if (result != undefined)
				{
					document.getElementById("txtTransTypeQuery").value = result.code;
					document.getElementById("txtTransTypeNameQuery").value = result.name;
				}
			}
			function SelectMOItem()
			{
				var result = window.showModalDialog("FMOSelectSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtMOCodeQuery").value = result.code;
				}
			}
			function ChangeToDropDownList(fromName, toName)
			{
				var fromobj = document.getElementById(fromName);
				var toobj = document.getElementById(toName);
				if (fromobj == undefined || toobj == undefined)
					return;
				toobj.selectedIndex = fromobj.selectedIndex;
			}
			
			
	        function checkSNCount(actionType)
            {  
                
                      
                var StartSn = document.getElementById("txtMORCardStartEdit");
                var StartSnCodeValue = StartSn.value;
                
             
                
                var snPrefix = document.getElementById("txtFirstStringEdit");
                var snPrefixValue = snPrefix.value;
                
              
                
                var EndSn = document.getElementById("txtMORCardEndEdit");
                var EndSnValue = EndSn.value;
                
                 var SnSeq = document.getElementById("txtSeq");
                var SnSeqValue = SnSeq.value;
                
               
                
    
                
                var itemCode = document.getElementById("txtInsideItemCodeEdit");
                var itemCodeValue = itemCode.value;
                
              
                            
                
                if (document.all.rdoSnScaleTenEdit.checked)
                {             
                    CheckedValue="10";
                }
                if (document.all.rdoSnScaleSixTeenEdit.checked)
                {
                   CheckedValue="16";
                
                }
                 if (document.all.rdoSnScaleThreeFourEdit.checked)
                {
                    CheckedValue="34";
                
                }                           
                
                var result = openPage('<%=Request.ApplicationPath%>/Warehouse/FMaterialQueryCheck.ashx?snPrefix='+snPrefixValue+'&StartSnCodeValue='+StartSnCodeValue+'&Action='+actionType+'&EndSnValue='+EndSnValue+'&SnSeqValue='+SnSeqValue+'&itemCodeValue='+itemCodeValue+'&CheckedValue='+CheckedValue);
                

                if(result=="true")
                {
                    var confirmDialogPath = '<%=Request.ApplicationPath%>/MOModel/ConfirmDialog.htm';
                    // the basic width
                    var rdBasicWidth = "420px";
                    // the basic height
                    var rdBasicHeight = "185px";
                    var _g_privateDialogFeatures = "status=no;center=yes;help=no;dialogWidth="+
                                                    rdBasicWidth+";dialogHeight="+
								                    rdBasicHeight+";scroll=yes;resize=no";								                    
            								        
                    var args = ' 将会生成的序列号个数超过5000，很有可能影响系统效率，\n是否继续？';
                    var returnValue = window.showModalDialog(confirmDialogPath, args, _g_privateDialogFeatures);
                   
                    if(returnValue == 0)
                    {
                        return false;
                    }
                    else
                    {
                    
                        return true;
                    }
                }
                return true;
            }
            
            function openPage(pageName)
            {
               var xmlHTTP=new ActiveXObject("Microsoft.XMLHTTP");

               xmlHTTP.open("GET",pageName,false);

               xmlHTTP.setRequestHeader("Content-Type","application/x-www-form-urlencoded");            

               xmlHTTP.send();      

               return xmlHTTP.responseText;      
            }
        			
		
            
    
			
		</script>
	</HEAD>
	<body  onload="InitPageWhenLoad()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 物料交易查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblInsideItemCodeQuery" runat="server">料号</asp:label></td>
								<td><asp:TextBox id="txtItemCodeQuery" runat="server" Width="150px" Type="material" cankeyin="true" CssClass="textbox"></asp:TextBox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOCodeQuery" runat="server" Visible="false" Width="0px"> 工单号</asp:label></td>
								<td><cc2:selectabletextbox id="txtMOCodeQuery" runat="server"  Type="mo" Visible="false" Width="0px" cankeyin="true" CssClass="textbox"></cc2:selectabletextbox></td>
										
								<td class="fieldNameLeft" noWrap><asp:label id="lblSNQuery" runat="server"> 序列号</asp:label></td>
								<td><asp:TextBox id="txtSNQuery" runat="server"  Width="130px" CssClass="textbox"></asp:TextBox></td>
										
								<td class="fieldNameLeft" noWrap><asp:label id="lblMDateQuery" runat="server"> 日期</asp:label></td>
								<TD><uc1:emesdate id="txtMDateQuery" runat="server"  width="80" CssClass="textbox"></uc1:emesdate></TD>
								
								<td class="fieldNameLeft" noWrap><asp:label id="lblUserCodeQuery" runat="server"> 用户</asp:label></td>
								<td><asp:textbox id="txtUserCodeQuery" runat="server" Width="80px" CssClass="textbox" ReadOnly=true></asp:textbox></td>
								
								<TD class="fieldName" noWrap width="100%"><asp:textbox id="txtSeq" runat="server" width="0px">序列号</asp:textbox><asp:label id="lbl2" runat="server" Visible="false"></asp:label></TD>
								
								<td align="center" colspan="2"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										tabindex="0" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="webGrid" TableLayout="Fixed">
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
								<ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				
						<tr class="normal">
					<td align="center">
					
						<table class="edit" height="100%" cellPadding="0" width="50%">
							<tr>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblInsideItemCodeEdit" runat="server">料号</asp:label></td>
								<td style="HEIGHT: 26px"><asp:textbox id="txtInsideItemCodeEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
		 <td  colspan=2><asp:label id="lblItemDesc" runat="server" ></asp:label></td>
																
		
		                       <td class="fieldName" noWrap><asp:label id="lblQty" runat="server">数量</asp:label></td>
								<td><asp:textbox id="txtQTY" runat="server" CssClass="textbox" Width="130px" ></asp:textbox></td>						
								
								
								</TD>
								<td class="toolBar"><Input  id="btnLockEdit" type="submit" value="锁定"  class="submitImgButton" runat="server"  onserverclick="btnLockEdit_Click"></td>
							</tr>
							<tr>
							<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblFirstStringEdit" runat="server">首字符串</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtFirstStringEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
		<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblMORCardStartEdit" runat="server">起始序列号</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMORCardStartEdit" runat="server" CssClass="require" Width="130px"></asp:textbox>						
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblMORCardEndEdit" runat="server">结束序列号</asp:label></td>
								<td style="HEIGHT: 26px"><asp:textbox id="txtMORCardEndEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								
								
								<td class="toolBar"><input  id="btnJSNEdit" type="submit" value="解析序列号"  class="submitImgButton" runat="server"  onserverclick="btnJSNEdit_Click"></td>
							</tr>
							<tr>
							<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblDateCode" runat="server">生产日期</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtDateCode" runat="server" CssClass="textbox" Width="130px"></asp:textbox></TD>
								
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblFactoryE" runat="server">厂商</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtFactoryE" runat="server" CssClass="textbox" Width="130px"></asp:textbox>
								</TD>
								
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblSupplierItemEdit" runat="server">厂商料号</asp:label></td>
								<td style="HEIGHT: 26px"><asp:textbox id="txtSupplierItemEdit" runat="server"  CssClass="textbox" Width="130px"></asp:textbox></td>
								
								
								<td class="toolBar"><input  id="btnTestQty" type="submit" value="试算数量" class="submitImgButton" runat="server"  onserverclick="btnTestQty_Click" ></td>
							</tr>
							<tr>
							<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblMaterialVersionEdit" runat="server">物料版本</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMaterialVersionEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></TD>
		<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblPCBAVersionEdit" runat="server">PCBA版本</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtPCBAVersionEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox>
								</TD>						
								
								<td class="fieldName" noWrap><asp:label id="lblBIOSVersionEdit" runat="server">BIOS版本</asp:label></td>
								<td><asp:textbox id="txtBIOSVersionEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>
								
								
								<td class="toolBar"><Input  id="btnTestEndSN" type="submit" value="试算结束序列号" runat="server" class="submitImgButton" onserverclick="btnTestEndSN_Click" ></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:checkbox id="chbRCardLengthEdit" runat="server" Text="序列号长度" AutoPostBack="true" OnCheckedChanged="chbRCardLengthEdit_CheckedChanged"/></asp:checkbox></td>
								<td><asp:textbox id="txtRCardLengthEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblSnScaleEdit" runat="server">序列号进制</asp:label></td>
								<td><asp:RadioButton id="rdoSnScaleTenEdit" runat="server" Text="10进制" AutoPostBack="true" OnCheckedChanged="rdoSnScaleTenEdit_CheckedChanged"/><asp:RadioButton id="rdoSnScaleSixTeenEdit" runat="server" Text="16进制" AutoPostBack="true" OnCheckedChanged="rdoSnScaleSixTeenEdit_CheckedChanged"/><asp:RadioButton id="rdoSnScaleThreeFourEdit" runat="server" Text="34进制" AutoPostBack="true" OnCheckedChanged="rdoSnScaleThreeFourEdit_CheckedChanged"/></asp:textbox></td>
		
		                     <td class="fieldName" noWrap><asp:label id="lblLotNo" runat="server">生产批号</asp:label></td>
								<td><asp:textbox id="txtLotNo" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>						
								
								<td class="toolBar"></td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar">
								<INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server" ></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
		</form>
	</body>
</HTML>
