<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UCNumericUpDown.ascx.cs" Inherits="BenQGuru.eMES.Web.UserControl.UCNumericUpDown" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD><asp:textbox id="num" onkeydown="javascript:UCNumericUpDown_CheckInputKeyDown()" onkeyup="javascript:UCNumericUpDown_CheckInputKeyUp()"
				runat="server" Width="108px" CssClass="textbox"></asp:textbox></TD>
		<TD><IMG height="21" src="<%=VirtualHostRoot%>skin/image/numericupdown.jpg" width="15" align="absMiddle" useMap="#UCNumericUpDownMap"
				border="0"></TD>
	</TR>
</TABLE>
<map name="UCNumericUpDownMap">
	<area shape="RECT" coords="1,1,15,11" href="javascript:UCNumericUpDown_Up()">
	<area shape="RECT" coords="1,11,15,21" href="javascript:UCNumericUpDown_Down()">
</map>
<script language="javascript">
function UCNumericUpDown_GetControl(){
	return document.getElementById(UCNumericUpDownID+":num") ;
}

function intval(str){
	try{
		return parseInt(str) ;
	}
	catch(e){
		return 0 ;
	}
}

function UCNumericUpDown_Up(){
	var obj = UCNumericUpDown_GetControl() ;
	var value = intval(obj.Number) + intval(obj.Increment) ;
	
	if( value > intval( obj.MaxValue ) ){
		value = intval( obj.MaxValue) ;
	}
	
	if( value < intval( obj.MinValue ) ){
		value = intval( obj.MinValue) ;
	}
	
	obj.value = value ;
	obj.Number = value ;
}

function UCNumericUpDown_Down(){
	var obj = UCNumericUpDown_GetControl() ;
	var value = intval(obj.Number) - intval(obj.Increment) ;
	
	if( value > intval( obj.MaxValue ) ){
		value = intval( obj.MaxValue) ;
	}
	
	if( value < intval( obj.MinValue ) ){
		value = intval( obj.MinValue) ;
	}

	obj.value = value ;
	obj.Number = value ;
}

function UCNumericUpDown_CheckInputKeyDown(){
	var key =event.keyCode;
	var accept = false ;
	
	if( key==8 || key==37 || key==38 || key==46 || (key>47 && key<58) || (key>95 && key<106) ){
		return true ;
	}
	else{
		event.returnValue = false ;
		event.cancelBubble = true ;
		return false ;
	}
	
}

function UCNumericUpDown_CheckInputKeyUp(){
	try{
		var obj = UCNumericUpDown_GetControl() ;
		obj.value = intval(obj.value) ;
	}
	catch(e){alert(e.message)}
}
</script>
