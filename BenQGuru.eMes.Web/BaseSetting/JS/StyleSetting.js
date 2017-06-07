var varBtnOK = document.getElementById("btnOK");
if (varBtnOK != null)
{ 
	varBtnOK.style.backgroundImage ='url("http://localhost/BaseSetting/Images/save.gif")';
	varBtnOK.onclick = function()
	{
		//alert(document.location.toString().indexOf("/", 3));
		//alert(document.location.pathname); 
		//alert(document.location.hostname);
		//alert(document.location.pathname);  
		//alert(varBtnOK.style.backgroundImage);
		//alert("Onclick event fire test ok!");
	}
	
}

var varBtnCancel = document.getElementById("btnCancel");
if (varBtnCancel != null)
{ 
	varBtnCancel.onclick = function()
	{
		alert("Onclick event fire test ok!");
	}
}
