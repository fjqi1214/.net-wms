<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="UCNumericUpDown.ascx.cs"
    Inherits="BenQGuru.eMES.Web.UserControl.UCNumericUpDown" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table1" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <asp:TextBox ID="num" onkeydown="javascript:UCNumericUpDown_CheckInputKeyDown()"
                onkeyup="javascript:UCNumericUpDown_CheckInputKeyUp()" runat="server" Width="108px"
                CssClass="textbox"></asp:TextBox>
        </td>
        <td>
            <img height="21" src="<%=VirtualHostRoot%>skin/image/numericupdown.jpg" width="15"
                align="absMiddle" usemap="#UCNumericUpDownMap" border="0">
        </td>
    </tr>
</table>
<map name="UCNumericUpDownMap">
    <area shape="RECT" coords="1,1,15,11" href="javascript:UCNumericUpDown_Up()">
    <area shape="RECT" coords="1,11,15,21" href="javascript:UCNumericUpDown_Down()">
</map>
<script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script language="javascript">
    function UCNumericUpDown_GetControl() {
        //return document.getElementById(UCNumericUpDownID+"_num") ;
        return $('#' + UCNumericUpDownID + "_num");
    }

    function intval(str) {
        try {
            return parseInt(str);
        }
        catch (e) {
            return 0;
        }
    }

    function UCNumericUpDown_Up() {
        var obj = UCNumericUpDown_GetControl();
        var value = intval(obj.attr("value")) + intval(obj.attr("Increment"));

        if (value > intval(obj.attr("MaxValue"))) {
            value = intval(obj.attr("MaxValue"));
        }

        if (value < intval(obj.attr("MinValue"))) {
            value = intval(obj.attr("MinValue"));
        }

        obj.attr("value", value);
        obj.attr("Number", value);
        obj.val(value);
    }

    function UCNumericUpDown_Down() {
        var obj = UCNumericUpDown_GetControl();
        var value = intval(obj.attr("Number")) - intval(obj.attr("Increment"));

        if (value > intval(obj.attr("MaxValue"))) {
            value = intval(obj.attr("MaxValue"));
        }

        if (value < intval(obj.attr("MinValue"))) {
            value = intval(obj.attr("MinValue"));
        }

        obj.attr("value", value);
        obj.attr("Number", value);
        obj.val(value);
    }

    function UCNumericUpDown_CheckInputKeyDown() {
        var key = event.keyCode;
        var accept = false;

        if (key == 8 || key == 37 || key == 38 || key == 46 || (key > 47 && key < 58) || (key > 95 && key < 106)) {
            return true;
        }
        else {
            event.returnValue = false;
            event.cancelBubble = true;
            return false;
        }

    }

    function UCNumericUpDown_CheckInputKeyUp() {
        try {
            var obj = UCNumericUpDown_GetControl();
            obj.attr("value", intval(obj.val()));
            obj.attr("Number", intval(obj.val()));
        }
        catch (e) { alert(e.message) }
    }
</script>
