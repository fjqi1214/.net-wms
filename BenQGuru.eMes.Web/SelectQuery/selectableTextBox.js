

function DoSelectableTextBoxClick(obj) {

    var object = obj||event.srcElement;// ? event.srcElement:event.target;
    if (object == null) {
        return;
    }

    var type = $(object).attr('DocumentType');
    var file;
    var name = $(object).attr('name')


    file = $(object).attr('TargetFile'); 
    if (file == null) {
        switch (type) {
            case "singlealloperation":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.Operation,BenQGuru.eMES.Domain&Code=OPCode&Desc=OPDescription";
                break;
            case "singEqpID":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Equipment.Equipment,BenQGuru.eMES.Domain&Code=EqpId&Desc=EqpDesc";
                break;
            case "Equiment":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Equipment.Equipment,BenQGuru.eMES.Domain&Code=EqpId&Desc=EqpDesc";
                break;
            case "singleoqccheckgroup":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.OQC.OQCCheckGroup,BenQGuru.eMES.Domain&Code=CheckGroupCode&Desc=CheckGroupCode";
                break;
            case "singleuser":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.User,BenQGuru.eMES.Domain&Code=UserCode&Desc=UserName";
                break; 
            case "usergroup":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.UserGroup,BenQGuru.eMES.Domain&Code=UserGroupCode&Desc=UserGroupDescription";
                break;
            case "rmabill":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.RMA.RMABill,BenQGuru.eMES.Domain&Code=RMABillCode&Desc=Memo";
                break;    
            case "CKItemCode":
                var reg = new RegExp(",", "g"); //创建正则RegExp对象  
                file = "FCKItemCodeSP.aspx?CKGroup=" + encodeURI($("input[name^='txtCKGroup']").first().val().replace(reg, ";"));
                break;
            case "model":
                file = "FModelSP.aspx";
                break;
            case "item":
                file = "FItemSP.aspx";
                break;
            case "material":
                file = "FMaterialSP.aspx";
                break;
            case "singleitem":
                file = "FSingleItemSP.aspx";
                break;
            case "singlematerial":
                file = "FSingleMaterialSP.aspx";
                break;
            case "operation":
                file = "FOPSP.aspx";
                break;
            case "segment":
                file = "FSegmentSP.aspx";
                break;
            case "momemo":
                file = "FMOMemoSP.aspx";
                break;
            case "singlesegment":
                file = "FSingleSegmentSP.aspx";
                break;
            case "stepsequence":
                file = "FStepSequenceSP.aspx";
                break;
            case "mo":
                file = "FMOSP.aspx";
                break;
            case "reworkmo":
                file = "FReworkMOSP.aspx";
                break;
            case "singlemo":
                file = "FSingleMOSP.aspx";
                break;
            case "singlereworkmo":
                file = "FSingleReworkMOSP.aspx";
                break;
            case "singlermareworkmo":
                file = "FSingleRMAReworkMOSP.aspx";
                break;
            case "resource":
                file = "FResourceSP.aspx";
                break;
            case "warehouseitem":
                file = "FWHItemSP.aspx";
                break;
            case "user":
                file = "FUserSP.aspx";
                break;
            case "usermail":
                file = "FUserMailSP.aspx";
                break;
            case "errorcode":
                file = "FErrorCodeSP.aspx";
                break;
            case "errorcausegroup":
                file = "FErrorCauseGroupSP.aspx";
                break;
            case "singlesymptom":
                file = "FSingleSymptomSP.aspx";
                break;
            case "singlebu":
                file = "FSingleBUSP.aspx";
                break;
            case "department":
                file = "FDepartmentSP.aspx";
                break;
            case "singledepartment":
                file = "FSingleDepartmentSP.aspx";
                break;
            case "remo":
                file = "FReMOSP.aspx";
                break;
            case "rmabill":
                file = "FRMABillSP.aspx";
                break;
            case "shelf":
                file = "FShelfSP.aspx";
                break;
            case "symptom":
                file = "FSymptomSP.aspx";
                break;
            case "factory":
                file = "FFactorySP.aspx";
                break;
            case "singlefactory":
                file = "FSingleFactorySP.aspx";
                break;
            case "lot":
                file = "FLotSP.aspx";
                break;
            case "rejectlot":
                file = "FLotSP.aspx?Status=oqclotstatus_reject";
                break;
            //            case "singlecustomer": 
            //                file = "FSingleCustomer.aspx"; 
            //                break; 
            //            case "customer": 
            //                file = "FCustomerSP.aspx"; 
            //                break; 

            case "singlecustomer":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.Customer,BenQGuru.eMES.Domain&Code=CustomerCode&Desc=CustomerName";
                break;
            case "customer":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.Customer,BenQGuru.eMES.Domain&Code=CustomerCode&Desc=CustomerName";
                break; 
            case "singleroute":
                file = "FSingleRouteSP.aspx";
                break;
            case "singleop":
                file = "FSingleOPSP.aspx?WithRoute=Y&RouteCode=" + $("input[name^='txtReworkRoute']").first().val();
                break;
            case "singleopwithoutroute":
                file = "FSingleOPSP.aspx?WithRoute=N";
                break;
            case "singleFopsp":
                file="FSingleOPSP.aspx";
                break;    
            case "reworkcode":
                file = "FReworkCodeSP.aspx";
                break;
            case "singlereworkcode":
                file = "FSingleReworkCodeSP.aspx";
                break;
            case "errorcode2oprework":
                file = "FErrorCode2OPReworkSP.aspx";
                break;
            case "mmodelcode":
                file = "FMModelCodeSP.aspx";
                break;
            case "bigline":
                file = "FBigLineSP.aspx";
                break;
            case "mmachinetype":
                file = "FMmachinetypeSP.aspx";
                break;
            case "singlevendor":
                file = "FSingleVendorSP.aspx";
                break;
            case "vendor":
                file = "FVendorSP.aspx";
                break;
            case "selectcomplex":
                var queryNo = name.indexOf("$");
                if (queryNo > 0) {
                    var lastString = name.substring(queryNo - 4, queryNo);

                    var textBox1 = "txtDataSource" + lastString;
                    var textBox2 = "txtDataType" + lastString;
                    var textBox3 = "txtDataCode" + lastString;
                    var textBox4 = "txtDataDesc" + lastString;

                    file = "FSelectComplex.aspx?datasource=" + document.getElementById(textBox1).value
		                    + "&datatype=" + document.getElementById(textBox2).value
		                    + "&datacode=" + document.getElementById(textBox3).value
		                    + "&datadesc=" + document.getElementById(textBox4).value;
                }
                break;
            case "pomaterial":
                file = "FPOMaterialSP2.aspx";
                break;
            case "duty":
                file = "FDutyCodeSP.aspx";
                break;
            case "singleduty":
                file = "FSingleDutyCodeSP.aspx";
                break;
            case "productiontype":
                file = "FProductionTypeSP.aspx";
                break;
            case "oqclottype":
                file = "FOQCLotTypeSP.aspx";
                break;
            case "storagetype":
                file = "FStorageTypeSP.aspx";
                break;
            case "storage":
                file = "FStorageSP.aspx";
                break;
            case "singleoqccheckgroup":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.OQC.OQCCheckGroup,BenQGuru.eMES.Domain&Code=CheckGroupCode&Desc=CheckGroupDesc";
                break;
            case "singlestorage":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.Storage,BenQGuru.eMES.Domain&Code=StorageCode&Desc=StorageName";
                break;
            case "singleshiftcode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.Shift,BenQGuru.eMES.Domain&Code=ShiftCode&Desc=ShiftDescription";
                break;
            case "singleshifttypecode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.ShiftType,BenQGuru.eMES.Domain&Code=ShiftTypeCode&Desc=ShiftTypeDescription";
                break;
            case "singlebigss":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.BigSS,BenQGuru.eMES.Domain&Code=BigSSCode&Desc=BigSSDescription";
                break;
            case "singlemodel":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.MOModel.Model,BenQGuru.eMES.Domain&Code=ModelCode&Desc=ModelDescription";
                break;    
            case "stack":
                file = "FStackSP.aspx";
                break;
            case "shiftcode":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.Shift,BenQGuru.eMES.Domain&Code=ShiftCode&Desc=ShiftDescription";
                break;
            case "crewcode":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.ShiftCrew,BenQGuru.eMES.Domain&Code=CrewCode&Desc=CrewDesc";
                break;
            case "singlecrewcode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.ShiftCrew,BenQGuru.eMES.Domain&Code=CrewCode&Desc=CrewDesc";
                break;
            case "singleitemcode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.MOModel.Item,BenQGuru.eMES.Domain&Code=ItemCode&Desc=ItemDescription";
                break;

            case "singleexceptionCode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Performance.ExceptionCode,BenQGuru.eMES.Domain&Code=Code&Desc=Description";
                break;
            case "singlestepsequence":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.BaseSetting.StepSequence,BenQGuru.eMES.Domain&Code=StepSequenceCode&Desc=StepSequenceDescription";
                break;
            case "exceptionCode":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Performance.ExceptionCode,BenQGuru.eMES.Domain&Code=Code&Desc=Description";
                break;
            case "singlerealexceptionCode":
                file = "FsingleExceptionCodeSP.aspx?Date=" + $("#DateQuery").val()
				                            + "&ItemCode=" + $("#txtitemCode").val()
				                            + "&ShiftCode=" + $("#txtShiftCodeQuery").val()
				                            + "&SSCode=" + $("#txtSSQuery").val()
				                            + "&ExceptionCode=" + encodeURI($("input[name^='txtExceptionCodeEdit']").first().val());
                break;
            case "singleshiftcodeBySSCode":
                file = "FsingleShiftcodeBySSCodeSP.aspx?SSCode=" + encodeURI($("input[name^='txtSSEdit']").first().val());
                break;

            case "errorcodea":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.TSModel.ErrorCodeA,BenQGuru.eMES.Domain&Code=ErrorCode&Desc=ErrorDescription";
                break;
            case "errorcause":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.TSModel.ErrorCause,BenQGuru.eMES.Domain&Code=ErrorCauseCode&Desc=ErrorCauseDescription";
                break;
            case "exceptioncode":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Performance.ExceptionCode,BenQGuru.eMES.Domain&Code=Code&Desc=Description";
                break;
            case "storageattribute":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.StorageAttributeParam,BenQGuru.eMES.Domain&Code=ParamCode&Desc=ParamDesc";
                break;
            case "singlestack":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.SStack,BenQGuru.eMES.Domain&Code=StackCode&Desc=StackDesc";
                break;
            case "singleerror":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.TSModel.ErrorCodeA,BenQGuru.eMES.Domain&Code=ErrorCode&Desc=ErrorDescription";
                break;
            case "transferno":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.InvTransfer,BenQGuru.eMES.Domain&Code=TransferNO&Desc=Memo";
                break;
            case "transferno1":
                file = "FTransferSP.aspx";
                break;
            case "createuser":
                file = "FCreateUserSP.aspx";
                break;
            case "inspector":
                file = "FInspectorSP.aspx";
                break;
            case "errorcodewithgroup":
                file = "FErrorCodeWithGroupSP.aspx";
                break;
            case "errorcausewithgroup":
                file = "FErrorCauseWithGroupSP.aspx";
                break;
            case "msdlevel":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.MSDLevel,BenQGuru.eMES.Domain&Code=MHumidityLevel&Desc=MHumidityLevelDesc";
                break;
            case "singlemsdlevel":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.Warehouse.MSDLevel,BenQGuru.eMES.Domain&Code=MHumidityLevel&Desc=MHumidityLevelDesc";
                break;
            case "singleres":
                file = "FSingleRes.aspx?ssCode=" + $("#drpStepSequenceCodeEdit").val();
                break;
            case "singleMOCode":
                file = "FCommonSingleSP.aspx?Type=BenQGuru.eMES.Domain.MOModel.MO,BenQGuru.eMES.Domain&Code=MOCode&Desc=MODescription";
                break;
            case "MOCode":
                file = "FCommonMultiSP.aspx?Type=BenQGuru.eMES.Domain.MOModel.MO,BenQGuru.eMES.Domain&Code=MOCode&Desc=MODescription";
                break;
            case "singleASNSP":
                file = "FSingleASNSP.aspx";
                break;
            case "singleInvNoSP":
                file = "FSingleInvNoSP.aspx";
                break;
            case "singleInvNoMaterialSP":
                file = "FSingleInvNoMaterialSP.aspx?PickNo=" + $("#txtPickNoQuery").val();
                break;
            case "singleScrapMaterialSP":
                file = "FSingleScrapMaterialSP.aspx?storageCode="+$("#txtStorageCodeEidt").val();
                break;
            case "ASNSP":
                file = "FASNSP.aspx";
                break;
            case "singleWWpoMaterialSP":
                file = "FSingleWWpoInvNoSP.aspx?invCode=" + $("#txtInvNoEidt").val();
                break;
            case "WWpoMaterialSP":
                file = "FWWpoInvNoSP.aspx?invCode=" + $("#txtInvNoEidt").val();
                break;
            case "WWpoSP":
                file = "FWWpoSP.aspx?invCode=" + $("#txtInvNoEidt").val();
                break;
            case "StorageCode":
                file = "FStorageCodes.aspx";
                break;  
            default:
                file = $(object).attr('Target');
        }
    }

    var txtControl = $(object).closest("tbody").find("input:eq(0)");
    var txtControl1 = $(object).closest("tbody").find("input:eq(1)");
    if (txtControl == null) {
        return;
    }
    //var inputs = object.parentElement.parentElement.parentElement.all.tags("INPUT");

    var arg = new Object();
    arg.File = STB_Virtual_Path + "SelectQuery/" + file;
    //arg.Codes = txtControl.value ;
    arg.Codes = txtControl.val();
    arg.Others = txtControl1.val();
    arg.DataObject = object;
    arg.Window = window;
    var result = window.showModalDialog(STB_Virtual_Path + "SelectQuery/" + "FFrameSP.htm", arg, "dialogWidth:10px;dialogHeight:600px;scroll:no;help:no;status:no");
    if (result != null) {

        var resultCodes = result;
        var resultOthers = '';

        if (result.indexOf('{[(') > 0 && result.indexOf('{[()]}') <= 0) {
            resultCodes = result.substring(0, result.indexOf('{[('));
            resultOthers = result.substring(result.indexOf('{[(') + 3);
            resultOthers = resultOthers.substring(0, resultOthers.length - 3);
        }

        txtControl.val(resultCodes);
        txtControl1.val(resultOthers);

        if (document.all.cmdchangeMO != null) { document.all.cmdchangeMO.click(); }
    }
}
var CurrentCellID = '';
function SingleRowSelect(gId, cId, button) 
{
    var ocell = igtbl_getCellById(cId);
    if (ocell == null || ocell.Column.Key != 'Check') 
    {
        return;
    }

    if ("" != CurrentCellID) {
        var Lastcell = igtbl_getCellById(CurrentCellID);

        if (ocell != null && Lastcell != null) 
        {
            if (ocell.Row != null && Lastcell.Row != null) 
            {
                if (Lastcell.Row != ocell.Row)  //不是同一個Row
                {
                    Lastcell.Row.getCellFromKey("Check").setValue(false);
                    ocell.Row.getCellFromKey("Check").checked = true;
                    document.all.txtSelected.value = ocell.Row.getCellFromKey("Selector_UnselectedCode").getValue(); //取选中行对象的Code

                    CurrentCellID = cId;
                }
                else // 是同一個Row
                {
                    if (Lastcell.Row.getCellFromKey("Check").getValue() == true) //選中狀態
                    {
                        Lastcell.Row.getCellFromKey("Check").setValue(false);
                        document.all.txtSelected.value = "";
                    }
                    else //非選中狀態
                    {
                        Lastcell.Row.getCellFromKey("Check").checked = true;
                        document.all.txtSelected.value = ocell.Row.getCellFromKey("Selector_UnselectedCode").getValue(); //取选中行对象的Code
                    }
                }
            }
        }
    }
    else {
        var ocell = igtbl_getCellById(cId);
        if (ocell != null) {
            if (ocell.Row != null) {
                if (ocell.Row.getCellFromKey("Check") != null) 
                {
                    ocell.Row.getCellFromKey("Check").checked = true;
                    document.all.txtSelected.value = ocell.Row.getCellFromKey("Selector_UnselectedCode").getValue(); //取选中行对象的Code
                }
                CurrentCellID = cId;
            }
        }    
    }
}

function RowSelect(gId, cId, index) {
    var ocell = igtbl_getCellById(cId);

    var checkCell = ocell.Row.getCellFromKey("Check");

    checkCell.setValue(!checkCell.getValue());
}

var CurrentDefaultOrgCellID = '';
function SingleRowSelectForDefaultOrg(gId, cId, button) {

    var ocell = igtbl_getCellById(cId);
    if (ocell.Column.Key == 'DefaultOrg') {
        if (CurrentDefaultOrgCellID == "") {
            var oGrid = igtbl_getGridById(gId);
            var oRows = oGrid.Rows;
            for (i = 0; i < oRows.length; i++) {
                if (oRows.getRow(i).getCellFromKey("DefaultOrg").getValue() == true)
                    CurrentDefaultOrgCellID = oRows.getRow(i).getCellFromKey("DefaultOrg").Element.id;
            }
        }
        if (CurrentDefaultOrgCellID != "") {
            var Lastcell = igtbl_getCellById(CurrentDefaultOrgCellID);
            Lastcell.Row.getCellFromKey("DefaultOrg").setValue(false);
        }
        CurrentDefaultOrgCellID = cId;

        //set txtOthers
        var others = document.getElementById('txtOthers').value;
        if (others.indexOf(')') > 0) {
            others = others.substring(others.indexOf(')') + 1);
        }
        others = '(' + ocell.Row.cells[2].getValue() + ')' + others;
        document.getElementById('txtOthers').value = others;
    }
}

function returnSelectedOrg(errMsg) {
    try {
        //search selected default org
        var oGrid =  $find('gridSelected');
        var oRows = oGrid.get_rows();
        var defaultOrgID = "";
        for (i = 0; i < oRows.get_length(); i++) {
            if (oRows.get_row(i).get_cellByColumnKey("DefaultOrg").get_value() == true || oRows.get_row(i).get_cellByColumnKey("DefaultOrg").get_value() == "true") {
                defaultOrgID = '(' + oRows.get_row(i).get_cellByColumnKey("Selector_SelectedCode").get_value() + ')';
            }
        }

        //record selected default org id to txtOthers
        var others = $('#txtOthers').val();
        if (others.indexOf(')') > 0) {
            others = others.substring(others.indexOf(')') + 1);
        }
        others = defaultOrgID + others;
        $('#txtOthers').val(others);

        //return value
        if (others.indexOf(')') > 0) {
            window.parent.returnValue = $('#txtSelected').val() + '{[(' + $('#txtOthers').val() + ')]}';
            window.parent.close();
        }
        else {
            alert(errMsg);
        }

        return false;
    }
    catch (e) {
    }
}