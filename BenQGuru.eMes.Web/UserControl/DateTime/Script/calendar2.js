<script language="jscript">
var sMon = new Array(12);
	sMon[0] = "Jan";
	sMon[1] = "Feb";
	sMon[2] = "Mar";
	sMon[3] = "Apr";
	sMon[4] = "May";
	sMon[5] = "Jun";
	sMon[6] = "Jul";
	sMon[7] = "Aug";
	sMon[8] = "Sep";
	sMon[9] = "Oct";
	sMon[10] = "Nov";
	sMon[11] = "Dec";

function calendar1(t)
{
	var sPath = "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue;
	strFeatures = "dialogWidth=206px;dialogHeight=206px;center=yes;help=no;status=no";
	if(t!=null)
	{
		sTemp = t.value;
		var objRegular=/\-/g;
		sTemp=sTemp.replace(objRegular,"/");
	}

	sDate = showModalDialog(sPath,sTemp,strFeatures);
	if(sDate.length!=0)
	{
		strReturnValue=formatDate(sDate, 0);
		if(t!=null && strReturnValue!="" && strReturnValue!=null)
		{
			t.value = strReturnValue;
		}
    }
    
    return strReturnValue;
}

function calendar1(t,bIncludeTime)
{
	var sPath = "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue;
	
	if(t!=null)
	{
		sTemp = t.value;
		var objRegular=/\-/g;
		sTemp=sTemp.replace(objRegular,"/");
	}
	
	if(bIncludeTime)
	{
		strFeatures = "dialogWidth=206px;dialogHeight=226px;center=yes;help=no;status=no";
		sTemp += "||Time";
	}
	else
	{
		strFeatures = "dialogWidth=206px;dialogHeight=206px;center=yes;help=no;status=no";
	}

	sDate = showModalDialog(sPath,sTemp,strFeatures);
	if(sDate.length!=0)
	{
		if(bIncludeTime)
		{
			strReturnValue=formatDate(sDate, true);
		}
		else
		{
			strReturnValue=formatDate(sDate, false);
		}

		if(t!=null && strReturnValue!="" && strReturnValue!=null)
		{
			t.value = strReturnValue;
		}
    }

    return strReturnValue;
}


function calendarCell(t)
{
	var sPath = "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue="";
	if(t==null)
	{
		return;
	}
	strFeatures = "dialogWidth=206px;dialogHeight=206px;center=yes;help=no;status=no";
	sTemp = t.getValue();
	var objRegular=/\-/g;
	if(sTemp==null)
	{
		sTemp="";
	}
	else
	{
		sTemp=sTemp.replace(objRegular,"/");
	}

	sDate = showModalDialog(sPath,sTemp,strFeatures);
	if(sDate.length!=0)
	{
		strReturnValue=formatDate(sDate, 0);
		if(strReturnValue!="" && strReturnValue!=null)
		{
			t.setValue(strReturnValue);
		}
    }
    
    return strReturnValue;
}

function calendarCell(t,bIncludeTime)
{
	var sPath = "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue="";
	if(t==null)
	{
		return;
	}

	sTemp = t.getValue();
	var objRegular=/\-/g;
	if(sTemp==null)
	{
		sTemp="";
	}
	else
	{
		sTemp=sTemp.replace(objRegular,"/");
	}

	if(bIncludeTime)
	{
		strFeatures = "dialogWidth=206px;dialogHeight=226px;center=yes;help=no;status=no";
		sTemp += "||Time";
	}
	else
	{
		strFeatures = "dialogWidth=206px;dialogHeight=206px;center=yes;help=no;status=no";
	}

	sDate = showModalDialog(sPath,sTemp,strFeatures);

	if(sDate.length!=0)
	{
		if(bIncludeTime)
		{
			strReturnValue=formatDate(sDate, true);
		}
		else
		{
			strReturnValue=formatDate(sDate, false);
		}

		if(strReturnValue!="" && strReturnValue!=null)
		{
			t.setValue(strReturnValue);
		}
    }
    
    return strReturnValue;
}

function checkDate(t) 
{
	dDate = new Date(t.value);
	if (dDate == "NaN") 
	{
		t.value = "";
		return;
	}

	iYear = dDate.getFullYear();

	if ((iYear > 1899)&&(iYear < 1950))
	{
		sYear = "" + iYear + "";
		if (t.value.indexOf(sYear,1) == -1) 
		{
			iYear += 100;
			sDate = (dDate.getMonth() + 1) + "/" + dDate.getDate() + "/" + iYear;
			dDate = new Date(sDate);
		}
	}



	t.value = formatDate(dDate);
}

function formatDate(sDate,bNeedTime) 
{
	var sScrap = "";
	var dScrap = new Date(sDate);
	if (dScrap == "NaN") return sScrap;
	
	iDay = dScrap.getDate();
	iMon = dScrap.getMonth();
	iYea = dScrap.getFullYear();

	sScrap = iYea + "-" + (iMon + 1) + "-" + iDay;
	if(bNeedTime && sDate.indexOf(":")>=0)
	{
		sScrap += " " + dScrap.getHours() + ":" + dScrap.getMinutes();
		if(dScrap.getSeconds()!=0)
		{
			sScrap += ":" + dScrap.getSeconds();
		} 
	}
	
	return sScrap;
}
</script>