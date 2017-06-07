
function GetSelectAllContainer(gridName)
{	
	var gridHeader = GetWebGridTableByIndex(gridName , 0) ;
	if(gridHeader ==  null){
		return null ;		
	}

	var cell ;
	try
	{
		cell = gridHeader.rows[0].cells[0] ;
	}
	catch(e){}

	return cell ;
}


function GetWebGridContentTable(gridName){
	var obj = GetWebGridTableByIndex(gridName , 1 ) ;
	return obj ;
}

function GetWebGridTableByIndex(gridName , index ){
	var obj ;
	try{
		var tbl = document.getElementById(gridName + "_main");
		var objs = tbl.all.tags("TABLE");
		obj = objs[index] ;
	}
	catch(e){}
	
	return obj ;
}

function GetSelectAllControl(controlName){
	try{
		var obj = document.getElementById(controlName) ;
		return obj ;
	}
	catch(e){}
	return null ;
}

function ResetSelectAllPosition(controlName , gridName){
	
	try{	
	
		var cell = GetSelectAllContainer(gridName) ;
		
		if(cell == null){
			return false ;
		}
		
		var selectall = GetSelectAllControl(controlName) ;
				
		if(selectall==null){
			return false ;
		}
				
		selectall.parentElement.style.display = "none"
		cell.innerHTML = "" ;
		cell.appendChild( selectall ) ;
		selectall.gridName = gridName ;

	}
	catch(e){}

	try{
		var tbl = document.getElementById(gridName + "_main");
		tbl.oncontextmenu = function(){
			event.returnValue = false ;
			event.cancelBubble = true ;
		}
	}
	catch(E){
	}

}


window.onload = function DoWindowLoad(){
	try{
		
		var objs = document.body.all.tags("INPUT") ;
		for(var i=0 ;i<objs.length ;i++){
			if(objs[i].type =="text" && (!objs[i].disabled ) && (!objs[i].readOnly)){
				objs[i].focus();
				break;
			}
		}
	}
	catch(e){
	}
//	document.oncontextmenu = function(){
//		event.returnValue = false ;
//		event.cancelBubble = true ;
//	}
}

function showDialog(type)
{
	var owidth=window.screen.width;
	var oheight=window.screen.height;
				
	var width=0;
	var height=0;
				
	if(owidth==800)
	{
		if( type == 6 )
		{
			width=350;
			height=350;
		}
		if( type == 7 )
		{
			width=550;
			height=550;
		}
	}
	else
	{
		if( type == 6 )
		{
			width=350;
			height=350;
		}
		if( type == 7 )
		{
			width=850;
			height=550;
		}
	}
				
	var strFeature="dialogWidth:"+width+"px;"+"dialogHeight:"+height+"px;center:yes;help:no;status:no;";
	return strFeature;
}


//下载导入模板
function DownLoadFile() {
    //debugger;
    try {
        var hf = document.all.aFileDownLoad.href;
        if (hf == "") return;
        var path;
        var pix = hf.substr(0, 4);
        if (pix.toLowerCase() == "file") path = hf.substr(8, hf.length - 8);
        else if (pix.toLowerCase() == "http") path = hf;
        var fl = path.split('/');
        var file = fl[fl.length - 2] + '/' + fl[fl.length - 1];

        var frameDown = $('<a></a>');
        frameDown.appendTo($('form'));
        frameDown.html('<span></span>');
        frameDown.attr('href', "http://" + fl[fl.length - 4] + "/" + fl[fl.length - 3] + "/FDownload.aspx?&fileName=" + escape(file));
        frameDown.children().click();
        frameDown.remove();
        return false;

    } catch (e) { alert(e.description); }
}