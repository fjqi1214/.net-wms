function OpenColorPickerWin(baseUrl, colorSample, colorValue)
{
	var ret = window.showModalDialog(baseUrl + "/UserControl/ColorPicker/colorpickerui.htm", "", "dialogHeight:400px;dialogWidth:460px");
	if (ret != null && ret != undefined)
	{
		document.getElementById(colorSample).style.backgroundColor = ret;
		document.getElementById(colorValue).value = ret;
	}
}
