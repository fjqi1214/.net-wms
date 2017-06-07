function DoSelectableTextBoxClick(){
	var object=event.srcElement ;
	if(object==null){
		return ;
	}
	
	var type = object.DocumentType; 
	var file ;
	switch(type){
		case "model":
			// model;
			file = "FModelSP.aspx" ;
			break;
		case "item":
			// item
			file = "FItemSP.aspx" ;
			break;
		case "op":
			file = "FOPSP.aspx";
			break;
		case "segment":
			file = "FSegmentSP.aspx";
			break;
		case "stepsequence":
			file = "FStepSequenceSP.aspx";
			break;
		case "mo":
			file = "FMOSP.aspx";
			break;
		case "resource":
			file = "FResourceSP.aspx";
			break;			
	}
	var txtControl = object.parentElement.parentElement.parentElement.all.tags("INPUT")[0];
	if(txtControl == null){
		return ;
	}

	var arg = new Object();
	arg.File = file ;
	arg.Codes = txtControl.value ;
	var result = window.showModalDialog("FFrameSP.htm", arg ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
	if(result!=null){
		txtControl.value = result ;
	}
}