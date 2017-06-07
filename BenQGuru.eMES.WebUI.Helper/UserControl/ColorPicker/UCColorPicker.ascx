<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCColorPicker.ascx.cs" Inherits="BenQGuru.eMES.Web.UserControl.UCColorPicker" %>
<script src="<%=GetColorPickerJsFileUrl()%>"></script>
<table>
    <tr>
        <td runat="server" id="tdColorSample" style="border-right: window   1px   inset; border-top: window   1px   inset;
                    font-size: 2px; border-left: window   1px   inset; width: 50px; cursor: default;
                    border-bottom: window   1px   inset;">&nbsp;&nbsp;&nbsp;&nbsp;<input type="hidden" id="hidColorPicker" runat="server" /></td>
        <td><IMG id="img1" style="CURSOR: hand" onclick="OpenColorPickerWin('<%=GetBaseUrl()%>','<%=ColorSampleCellId%>','<%=ColorValueControlId%>');return false;" src="<%=GetBaseUrl()%>skin/Image/go.gif" align="absMiddle"></td>
    </tr>
</table>