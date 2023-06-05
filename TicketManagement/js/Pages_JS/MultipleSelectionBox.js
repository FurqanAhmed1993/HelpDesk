function FillListBoxByReference(listBoxReference, res) {
    $(listBoxReference).empty().append('');
    $(res).each(function () {
        $(listBoxReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}


function OnLoadControl() {
    GetDepartmentsForMultiSelectionEmployee();
    $(".ddlDepartmentCC").change(function () {
        getSourceEmployee($(this).val());
    });
}


function GetDepartmentsForMultiSelectionEmployee() {
    var service = new CyberTicketService.TicketService();
    service.getDepartments(onGetDepartmentsForMultiSelectionEmployee, null, null);
}
function onGetDepartmentsForMultiSelectionEmployee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDepartmentCC", res);
    $(".ddlDepartmentCC").change();

}

function getSourceEmployee(DepartmentId) {
    var MainResponse = [];
    $('.ListBoxDestination option').each(function () {
        var Response = new Object();
        Response.User_Code = $(this).val();
        MainResponse.push(Response);
    });
    var service = new CyberTicketService.TicketService();
    service.getSourceEmployeeforCC(JSON.stringify(MainResponse), DepartmentId, ongetSourceEmployee, null, null);
}
function ongetSourceEmployee(result) {
    var res = jQuery.parseJSON(result);
    FillListBoxByReference(".ListBoxSource", res);
}



function OnClientClick_Add() {
    var MainResponse = [];
    $('.ListBoxSource :selected').each(function (i, selected) {
        var Response = new Object();
        Response.User_Code = $(this).val();
        MainResponse.push(Response);
    });
    var MainResponseDesc = [];
    $('.ListBoxDestination option').each(function () {
        var Response = new Object();
        Response.User_Code = $(this).val();
        MainResponseDesc.push(Response);
    });
    SelectedEmployee(MainResponse, MainResponseDesc, $(".ddlDepartmentCC").val());
    //$(".ListBoxSource option:selected").removeAttr("selected");
    $(".ListBoxSource option:selected").each(function () {
        $(this).remove(); //or whatever else
    });
}
function OnClientClick_Remove() {

    $(".ListBoxDestination option:selected").each(function () {
        $(this).remove(); //or whatever else
    });
    var MainResponse = [];
    $('.ListBoxDestination option').each(function () {
        var Response = new Object();
        Response.User_Code = $(this).val();
        MainResponse.push(Response);
    });

    getSourceEmployee($(".ddlDepartmentCC").val());
}

function SelectedEmployee(Source, Destination, DepartmentId) {
    var service = new CyberTicketService.TicketService();
    service.getSelectedEmployeeforCC(JSON.stringify(Source), JSON.stringify(Destination), DepartmentId, ongetSelectedEmployee, null, null);
}
function ongetSelectedEmployee(result) {
    var res = jQuery.parseJSON(result);
    FillListBoxByReference(".ListBoxDestination", res);
}