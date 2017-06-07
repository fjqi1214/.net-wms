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

function getHtmlFile()
{
	var my_array = new Array();
	var htmlStr="";
	var s = window.location.href
	//
	my_array = s.split("/");
	htmlStr=my_array[0];
	for(i=1;i<4;i++)
	{
		htmlStr=htmlStr+"/"+my_array[i];
	}
	
	htmlStr=htmlStr+"/UserControl/DateTime/Script/calendar1.htm"
	return htmlStr;
}

function calendar1(t)
{

	//modify by tilancs yao 2004/11/04 
	//if (t.readOnly)
	//{
		//alert("ok");
		//return;   
	//}
	
	
	var sPath =getHtmlFile()// "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue;
	strFeatures = "dialogWidth=206px;dialogHeight=213px;center=yes;help=no;status=no";
	
	if(t!=null)
	{
		sTemp = t.value;
		var objRegular=/\-/g;
		sTemp=sTemp.replace(objRegular,"/");
	}

	sDate = showModalDialog(sPath,sTemp,strFeatures);
	//
	//alert('change'+sDate);
	
	if(sDate.length!=0)
	{
		strReturnValue=formatDate(sDate);
		//alert('returnvlaue'+ strReturnValue);
		
		if(t!=null && strReturnValue!="" && strReturnValue!=null)
		{
			t.value = strReturnValue;
		}
    }
    else
    {
        t.value="";
    }
    return strReturnValue;
}

function calendarCell(t)
{
	var sPath =getHtmlFile()// "../../Script/calendar1.htm";
	var sTemp="";
	var strReturnValue="";
	if(t==null)
	{
		return;
	}
	//var sPath = "../../function/calendar1.htm";
	strFeatures = "dialogWidth=206px;dialogHeight=206px;center=yes;help=no;status=no";
	sTemp = t.getValue();
	var objRegular=/\-/g;
	sTemp=sTemp.replace(objRegular,"-");
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
			sDate = (dDate.getMonth() + 1) + "-" + dDate.getDate() + "-" + iYear;
			dDate = new Date(sDate);
		}
	}



	t.value = formatDate(dDate);
}

function formatDate(sDate) 
{
	//alert('format');
	var sScrap = "";
	var dScrap = new Date(sDate);
	if (dScrap == "NaN") return sScrap;
	iDay = dScrap.getDate();
	iMon = dScrap.getMonth();
	iYea = dScrap.getFullYear();
	
	iMon++;
	if(iDay < 10)	iDay = "0" + iDay;
	if(iMon < 10)	iMon = "0" + iMon;

	sScrap = iYea + "-" + iMon + "-" + iDay ;
	
	return sScrap;
}


</script>