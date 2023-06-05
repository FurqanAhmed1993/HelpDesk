
//ProgressBarShow();
//ProgressBarHide();

let isContact = '';
let isEmail = '';

var ArrayImgURL = [];

var TicketStatus =
{
    New: 1,
    Cancel: 2,
    Closed: 3,
    Working: 4,
    Resolved: 5,
    OnHold: 6,
    ReOpen: 7,
    Junk: 8,
    NotAssigned: 9,
    Assigned: 10,
    Escalated: 11
}

var TicketType =
{
    External: 1,
    Internal: 2
}

var MethodOfContact =
{
    Email: 1,
    Fax: 2,
    Phone: 3,
    System: 4

}

function FileUpload(FileInput) {

    var files = FileInput.files;
    var data = new FormData();
    var ReturnFilePath;
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }


    $.ajax({
        url: "/UploadHandler.ashx",
        type: "POST",
        data: data,
        async: false,
        contentType: false,
        processData: false,
        success: function (result) {
            ReturnFilePath = result;
        },
        error: function (err) {
        }

    });
    return ReturnFilePath;

}

function OnLoad() {

    $('.btn_Save').hide();
    $(".div_Attachment").hide();

    var UserId = $('.hf_UserId').val() + "00" + $('.hf_UserId').val();
    DeleteAtachmentByTargetID(UserId);

    //debugger;
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var hfTicketCustomerId = $(".hfTicketCustomerId").val();
    var hfStatusId = $(".hfStatusId").val();
    $('.pnlHideShow').show();
    $('.SearchTicketPanel').show();

    if (hfStatusId != TicketStatus.Junk.toString() && hfStatusId != "0" && hfStatusId != "") {
        $('.SearchTicketPanel').hide();
        $('.btn_Save').show();
    }
    ArrayImgURL = [];
    //if (hfStatusId !== TicketStatus.Junk.toString()) {
    //    DisableControls();
    //    $('.DivHideOnEmail').hide();
    //    $('.lblServiceType_Child1').hide();
    //    $('.lblServiceSubtype_Child1').hide();
    //    $(".ddlDepartment").attr('disabled', 'disabled');
    //    $(".ddlLevel").attr('disabled', 'disabled');
    //    $(".ddlAssignee").attr('disabled', 'disabled');
    //}

    $('.btnCancelSearch').hide();

    //$('.txtName').prop('disabled', true);
    //$('.txtContact').prop('disabled', true);
    //$('.txtEmail').prop('disabled', true);
    //$('.txtAlternativeNumber').prop('disabled', true);
    //$('.txtAddress').prop('disabled', true);


    $(".DivPortDetails").hide();
    $(".DivSlotPONONT").hide();
    $(".DivlblServiceType_Master").hide();
    $(".DivlblServiceSubtype_Master").hide();

    $(".txtContact").focusout(function () {
        var contact = $(".txtContact").val();
        GetCustomerContact(contact);
    });

    $(".txtEmail").focusout(function () {
        var email = $(".txtEmail").val();
        GetCustomerEmail(email);
    });

    $(".btnSearch").click(function () {
        isContact = '';
        isEmail = '';
        if ($('.txtSearchContact').val() !== "" || $('.txtSearchEmail').val() !== "") {
            $('.btnCancelSearch').show();
            var contactSearch = $('.txtSearchContact').val().trim();
            var emailSearch = $('.txtSearchEmail').val().trim();
            GetSearchCustomer(contactSearch, emailSearch);
        }
    });

    $(".btnCancelSearch").click(function () {
        $('.txtSearchContact').val("");
        $('.txtSearchEmail').val("");
        $('.txtName').val("");
        $('.txtContact').val("");
        $('.txtEmail').val("");
        $('.txtAddress').val("");
        $('.txtToEmail').val("");
        $('.txtAlternativeNumber').val("");
        $('.btnCancelSearch').hide();
        $('.btn_Save').hide();
        isContact = '';
        isEmail = '';
    });


    $(".ddlRequestMode").change(function () {
        HideShowPOCDiv();
    });

    $(".ddlRequestType").change(function () {
        getCategory($(".ddlRequestType").val(), $(this).val());
        getSubcategory("0", $(this).val());
    });

    $(".ddlCategory").change(function () {
        getSubcategory($(".ddlCategory").val(), $(this).val());
    });

    $(".ddlSubcategory").change(function () {
        MakeTitle();
        SetDynamicFields($(this).val());
        //if (($('.ddlCategory :selected').text() == "Single Declaration (SD) - Exports" && $('.ddlSubcategory :selected').text() == "Filling & Submission") || ($('.ddlCategory :selected').text() == "Single Declaration (SD) - Exports" && $('.ddlSubcategory :selected').text() == "Hold due to RO/NOC")) {
        //    $('.divRegAuthority').show();
        //    GetRegularityAuthority();
        //}
        //else {
        //    $('.divRegAuthority').hide();
        //    GetRegularityAuthority();
        //}
        $('input.OGA').hide();
        $('input.OGA').hide();
        $(".div_Attachment").hide();
    });


    $(".ddlDepartment").change(function () {
        getAssigneeByDepartmentId($(this).val(), 0);
    });



    $('#dynamicFields').on('change', '.ReportedBy', function () {

        if ($('.ReportedBy :selected').text() == "Bank") {   // Bank
            $('#dynamicFields').find('.Bank').removeAttr('disabled');
        }
        else {
            //$('#dynamicFields').find('.Bank').val("0");
            //$('#dynamicFields').find('.Bank').attr('disabled', 'disabled');
        }

        if ($('.ReportedBy :selected').text() == "OGA") {   // OGA
            $('#dynamicFields').find('.OGA').removeAttr('disabled');
            $('#dynamicFields').find('.OGA2').removeAttr('disabled');
        }
        else {
            //$('#dynamicFields').find('.OGA').val("0");
            //$('#dynamicFields').find('.OGA').attr('disabled', 'disabled');

            //$('#dynamicFields').find('.OGA2').val("0");
            //$('#dynamicFields').find('.OGA2').attr('disabled', 'disabled');
        }

    });


    $('#chkNewCustomer').change(function () {
        if (this.checked) {
            $('.btn_Save').show();
            $('#pnlSearch').hide();
            $('.txtName').prop('disabled', false);
            $('.txtContact').prop('disabled', false);
            $('.txtEmail').prop('disabled', false);
            $('.txtAlternativeNumber').prop('disabled', false);
            $('.ddlCity').prop('disabled', false);
            $('.txtAddress').prop('disabled', false);

            $('.txtSearchContact').val("");
            $('.txtSearchEmail').val("");
            $('.txtName').val("");
            $('.txtContact').val("");
            $('.txtEmail').val("");
            $('.ddlCity').val(0);
            $('.txtAlternativeNumber').val("");
            $('.txtAddress').val("");
        } else {
            $('.btn_Save').hide();
            $('#pnlSearch').show();
            $('.txtName').val("");
            $('.txtContact').val("");
            $('.txtEmail').val("");
            $('.ddlCity').val(0);
            $('.txtAlternativeNumber').val("");
            $('.txtAddress').val("");
            $('.txtName').prop('disabled', true);
            $('.txtContact').prop('disabled', true);
            $('.txtEmail').prop('disabled', true);
            $('.ddlCity').prop('disabled', true);
            $('.txtAlternativeNumber').prop('disabled', true);
            $('.txtAddress').prop('disabled', true);
        }
    });

    if (hf_TicketMasterId != "0" && hf_TicketMasterId != "" && hf_TicketMasterId != null) {
        getTicketDataOnEdit(hf_TicketMasterId);
        $(".Btn_UploadDiv_New").hide();

        $('.txtName').prop('disabled', false);
        $('.txtContact').prop('disabled', false);
        $('.txtEmail').prop('disabled', false);
        $('.ddlCity').prop('disabled', false);
        $('.txtAlternativeNumber').prop('disabled', false);
        $('.txtAddress').prop('disabled', false);
    }
    else {
        $(".Btn_UploadDiv").hide();
        $(".RptAttachment").hide();
        getInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity();
    }

    $(".txtEmail").change(function () {
        $(".txtToEmail").val($(".txtEmail").val());
    });

}



function MakeTitle() {

    if ($('.ddlCategory').val() === "0") {
        $('.txtTitle').val("");
    } else {
        let category = $('.ddlCategory :selected').text();
        let subCategory = ""
        if ($('.ddlSubcategory').val() !== "0") {
            subCategory = $('.ddlSubcategory :selected').text();
        }
        let Title = category + " : " + subCategory;
        $('.txtTitle').val(Title);
    }

}



function GetSearchCustomer(contactSearch, emailSearch) {
    var service = new CyberTicketService.TicketService();
    service.GetSearchCustomer(contactSearch, emailSearch, ongetCustomer, null, null);
}
function ongetCustomer(result) {
    var res = jQuery.parseJSON(result);
    if (res[0].CustomerId != "0") {
        debugger
        $(".hfTicketCustomerId").val(res[0].CustomerId);
        $(".txtName").val(res[0].CustomerName);
        $(".txtContact").val(res[0].ContactNo);
        $(".txtEmail").val(res[0].EmailAddress);
        $(".ddlCity").val(res[0].CityId);
        $(".txtToEmail").val(res[0].EmailAddress);
        $(".txtAddress").val(res[0].Address);
        $(".txtAlternativeNumber").val(res[0].AlternativeNumber);
        $('.btn_Save').show();
        isContact = '';
        isEmail = '';
    } else {
        $('.btn_Save').hide();
        $('.txtName').val("");
        $('.txtContact').val("");
        $('.txtEmail').val("");
        $('.ddlCity').val(0);
        $('.txtAlternativeNumber').val("");
        $('.txtAddress').val("");
        AlertBox('Alert', 'No result found.', 'info');
    }

}

function GetCustomerContact(contact) {
    var service = new CyberTicketService.TicketService();
    service.GetCustomerContact(contact, ongetCustomerContact, null, null);
}
function ongetCustomerContact(result) {
    var res = jQuery.parseJSON(result);
    if (res[0].ContactNo != "") {
        isContact = '1';
    } else
        isContact = '0';
}

function GetCustomerEmail(email) {
    var service = new CyberTicketService.TicketService();
    service.GetCustomerEmail(email, ongetCustomerEmail, null, null);
}
function ongetCustomerEmail(result) {
    var res = jQuery.parseJSON(result);

    if (res[0].EmailAddress != "") {
        isEmail = '1';
    } else
        isEmail = '0';
}





function GetExchangePOP(ProductId, CityId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetExchangePOP(ProductId, CityId, onGetExchangePOP, null, null);
}
function onGetExchangePOP(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSearchExchangePOP", res);
    ProgressBarHide();
}


function GetCity(RegionId) {
    var service = new CyberTicketService.TicketService();
    service.GetCity(RegionId, onGetCity, null, null);
}
function onGetCity(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSearchCity", res);
    $(".ddlSearchCity").change();
}

function HideShowPOCDiv() {
    if ($(".ddlRequestMode").val() == MethodOfContact.Phone && $(".ddlPrimaryIP").val() != "" && $(".ddlPrimaryIP").val() != "0" && $(".ddlPrimaryIP").val() != null) {
        $(".POCContacts").slideDown();
    }
    else {
        $(".POCContacts").slideUp();
    }
}



function DisableControls() {

    $(".ddlProduct").attr('disabled', 'disabled');
    $(".ddlPrimaryIP").attr('disabled', 'disabled');
    $(".ddlCircuitType").attr('disabled', 'disabled');
}

function GetPrimaryIPChangeEventData(TicketMasterId, ProductId, MasterId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetPrimaryIPChangeEventData(TicketMasterId, ProductId, MasterId, onGetPrimaryIPChangeEventData, null, null);
}
function onGetPrimaryIPChangeEventData(result) {
    var result_ = result.split('Split_');
    $(".btn_Save_Hide").show();
    //debugger;
    var HasLink = result_[5];
    if (HasLink != "") {
        debugger
        $(".btn_Save_Hide").hide();
        var HasLink_ = HasLink.split(',');
        if (HasLink_[0] == "0") {
            AlertBox("Alert", "Unable to Initiate Ticket because <br /> <strong> Ticket # : " + HasLink_[1] + "</strong> <br /> already opened against this link", "warning");
        }
        else {
            var Exchange_POP_value = result_[0];
            if (Exchange_POP_value != "0") {
                $(".lblExchangePOP").text(Exchange_POP_value);
            }
            else {
                $(".lblExchangePOP").text("");
            }
            var SlotPOnONT = result_[4];
            if (SlotPOnONT != "0") {
                $(".DivSlotPONONT").show();
                $(".lbl_SLOT_PON_ONT").text(SlotPOnONT);
            }
            else {
                $(".lbl_SLOT_PON_ONT").text("");
                $(".DivSlotPONONT").hide();
            }

            var POCDetails = jQuery.parseJSON(result_[1]);
            BindPOCDetailTable(POCDetails);

            if (result_[2] != "0") {
                var ServiceType_ServiceSubtype = jQuery.parseJSON(result_[2]);
                $(".lblServiceType_Master").text(ServiceType_ServiceSubtype[0].ServiceType);
                $(".lblServiceSubtype_Master").text(ServiceType_ServiceSubtype[0].ServiceSubType);
                $(".DivlblServiceType_Master").show();
                $(".DivlblServiceSubtype_Master").show();

            } else {
                $(".lblServiceType_Master").text("");
                $(".lblServiceSubtype_Master").text("");
                $(".DivlblServiceType_Master").hide();
                $(".DivlblServiceSubtype_Master").hide();
            }

            var PortDetails = jQuery.parseJSON(result_[3]);
            if (PortDetails != "0") {
                $(".DivPortDetails").slideDown();
                BindPortDetails(PortDetails);
            }
            else {
                BindPortDetails(null);
                $(".DivPortDetails").slideUp();
            }
        }
    }
    else {
        var Exchange_POP_value = result_[0];
        if (Exchange_POP_value != "0") {
            $(".lblExchangePOP").text(Exchange_POP_value);
        }
        else {
            $(".lblExchangePOP").text("");
        }
        var SlotPOnONT = result_[4];
        if (SlotPOnONT != "0") {
            $(".DivSlotPONONT").show();
            $(".lbl_SLOT_PON_ONT").text(SlotPOnONT);
        }
        else {
            $(".lbl_SLOT_PON_ONT").text("");
            $(".DivSlotPONONT").hide();
        }

        var POCDetails = jQuery.parseJSON(result_[1]);
        BindPOCDetailTable(POCDetails);

        if (result_[2] != "0") {
            var ServiceType_ServiceSubtype = jQuery.parseJSON(result_[2]);
            $(".lblServiceType_Master").text(ServiceType_ServiceSubtype[0].ServiceType);
            $(".lblServiceSubtype_Master").text(ServiceType_ServiceSubtype[0].ServiceSubType);
            $(".DivlblServiceType_Master").show();
            $(".DivlblServiceSubtype_Master").show();

        } else {
            $(".lblServiceType_Master").text("");
            $(".lblServiceSubtype_Master").text("");
            $(".DivlblServiceType_Master").hide();
            $(".DivlblServiceSubtype_Master").hide();
        }

        var PortDetails = jQuery.parseJSON(result_[3]);
        if (PortDetails != "0") {
            $(".DivPortDetails").slideDown();
            BindPortDetails(PortDetails);
        }
        else {
            BindPortDetails(null);
            $(".DivPortDetails").slideUp();
        }
    }
    ProgressBarHide();
}

function GetServiceType_ServiceSubtype(ProductId, MasterId, DetailId) {
    var service = new CyberTicketService.TicketService();
    //ProgressBarShow();
    service.GetServiceType_ServiceSubtype(ProductId, MasterId, DetailId, onGetServiceType_ServiceSubtype, null, null);
}
function onGetServiceType_ServiceSubtype(result) {
    if (result != "0") {
        var res = jQuery.parseJSON(result);
        $(".lblServiceType_Child").text(res[0].ServiceType);
        $(".lblServiceSubtype_Child").text(res[0].ServiceSubType);
    }
    else {
        $(".lblServiceType_Child").text("");
        $(".lblServiceSubtype_Child").text("");
    }
    //ProgressBarHide();
}

function BindPortDetails(res) {
    FillDropDownByReference(".ddlPortDetails", res);
}

function BindSlotPONONT(res) {
    $(".lblSlot").text(res[0].Slot);
    $(".lblPON").text(res[0].PON);
    $(".lblONT").text(res[0].ONT);
}

function getInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity(ongetInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity, null, null);
}

function ongetInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity(result) {
    /* debugger;*/
    var result_ = result.split('Split_');
    var Initiator = jQuery.parseJSON(result_[0]);
    var Priority = jQuery.parseJSON(result_[1]);
    var RequestType = jQuery.parseJSON(result_[2]);
    var RequestMode = jQuery.parseJSON(result_[3]);
    var Department = jQuery.parseJSON(result_[4]);
    var DefaultIdsofControls = result_[5].split('%');
    var Subcategory = jQuery.parseJSON(result_[6]);
    var City = jQuery.parseJSON(result_[7]);
    /*var Assignee = jQuery.parseJSON(result_[7]);*/

    var Did = DefaultIdsofControls[0];
    var Pid = DefaultIdsofControls[1];
    var RTid = DefaultIdsofControls[2];

    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId == "0") {
        var UserId = $(".hf_UserId").val();

    }
    FillDropDownByReference(".ddlDepartment", Department);
    FillDropDownByReference(".ddlPriority", Priority);
    FillDropDownByReference(".ddlRequestType", RequestType);
    FillDropDownByReference(".ddlRequestMode", RequestMode);
    FillDropDownByReference(".ddlSubcategory", Subcategory);
    FillDropDownByReference(".ddlCity", City);
    /*FillDropDownByReference(".ddlAssignee", Assignee);*/

    $(".ddlRequestMode").val(MethodOfContact.Phone);
    $(".ddlRequestMode").change();
    $(".ddlDepartment").val(Did);
    $(".ddlPriority").val(Pid);
    $(".ddlRequestType").val(RTid);

    getCategory($(".ddlRequestType").val());
    getAssigneeByDepartmentId($(".ddlDepartment").val());
    ProgressBarHide();

    if (hf_TicketMasterId == "0") {
        var UserId = $(".hf_UserId").val();
        var CustomerId = $(".hfTicketCustomerId").val();
        if (CustomerId != "0") {
        } else {
            try {
                $(".ddlAssignee").val(UserId);
            }
            catch (e) { }
        }
    }

}

function getTicketDataOnEdit(TicketMasterId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.getTicketDataOnEdit(TicketMasterId, ongetTicketDataOnEdit, null, null);
}
function ongetTicketDataOnEdit(result) {
    if (result != "") {
        $(".lblServiceType_Child").text("");
        $(".lblServiceSubtype_Child").text("");
        $(".lblServiceType_Master").text("");
        $(".lblServiceSubtype_Master").text("");
        $(".DivlblServiceType_Master").hide();
        $(".DivlblServiceSubtype_Master").hide();

        var result_ = result.split('Split_');
        var res = jQuery.parseJSON(result_[0]);
        //var StatusId = res[0].StatusId;
        //alert(StatusId);


        var res_Initiator = jQuery.parseJSON(result_[1]);
        var res_Initiator = jQuery.parseJSON(result_[1]);
        var res_Priority = jQuery.parseJSON(result_[2]);
        var res_RequestType = jQuery.parseJSON(result_[3]);
        var res_RequestMode = jQuery.parseJSON(result_[4]);
        var res_Department = jQuery.parseJSON(result_[5]);
        var res_Assignee = jQuery.parseJSON(result_[6]);
        var res_TicketTO = jQuery.parseJSON(result_[7]);
        var res_TicketCC = jQuery.parseJSON(result_[8]);
        var res_TicketAtachment = jQuery.parseJSON(result_[9]);
        var res_Category = jQuery.parseJSON(result_[10]);
        var res_Subcategory = jQuery.parseJSON(result_[11]);
        var res_City = jQuery.parseJSON(result_[13]);

        var CustomerId = res[0].CustomerId;
        if (CustomerId > 1) {
            $('.SearchTicketPanel').hide();
            $('.btn_Save').show();
        }

        var InitiatorId = res[0].InitiatorId;
        var PriorityId = res[0].PriorityId;
        var RequestTypeId = res[0].RequestTypeId;
        var RequestModeId = res[0].RequestModeId;
        var DepartmentId = res[0].DepartmentId;
        var CityId = res[0].CityId;
        var CategoryId = res[0].RequestTypeCategoryId;
        var Tittle = res[0].Tittle;
        var AssigneeId = res[0].AssigneeId;
        var TicketTypeId = res[0].TicketTypeId;
        var SubcategoryId = res[0].RequestTypeSubcategoryId;
        var StatusId = res[0].StatusId;

        if (TicketTypeId == TicketType.External) {
            $(".Description").html(res[0].Description);
            $(".div_TextDescription").hide();
            $(".div_Description").show();
        }



        $(".txtTitle").val(Tittle);
        FillDropDownByReference(".ddlPriority", res_Priority);
        $(".ddlPriority").val(PriorityId == null ? "0" : PriorityId);
        FillDropDownByReference(".ddlRequestType", res_RequestType);
        $(".ddlRequestType").val(RequestTypeId == null ? "0" : RequestTypeId);
        FillDropDownByReference(".ddlRequestMode", res_RequestMode);
        $(".ddlRequestMode").val(RequestModeId == null ? "0" : RequestModeId).change();
        FillDropDownByReference(".ddlDepartment", res_Department);
        $(".ddlDepartment").val(DepartmentId == null ? "0" : DepartmentId);
        FillDropDownByReference(".ddlCity", res_City);
        $(".ddlCity").val(CityId == null ? "0" : CityId);
        FillDropDownByReference(".ddlAssignee", res_Assignee);
        $(".ddlAssignee").val(AssigneeId == null ? "0" : AssigneeId);
        FillDropDownByReference(".ddlCategory", res_Category);
        $(".ddlCategory").val(CategoryId == null ? "0" : CategoryId);
        FillDropDownByReference(".ddlSubcategory", res_Subcategory);
        $(".ddlSubcategory").val(SubcategoryId == null ? "0" : SubcategoryId);
        $('.txtToEmail').val(res_TicketTO);
        $('.txtCCEmail').val(res_TicketCC);
        $('.hfInitiatorId').val(InitiatorId);
        $('.hfStatusId').val(StatusId);
        BindAttachmentRepeter(res_TicketAtachment);
        SetDynamicFields($(".ddlSubcategory").val());


        ProgressBarHide();
    }
    else {
        ProgressBarHide();
        AlertBoxSave("Alert!", "No Data found", "warning", "");
    }
}




function getTicketPrerequisite(TicketMasterId) {
    var service = new CyberTicketService.TicketService();
    service.getTicketPrerequisite(TicketMasterId, ongetTicketPrerequisite, null, null);
}
function ongetTicketPrerequisite(result) {
    var res = jQuery.parseJSON(result);
    SetPrerequisite(res);
}

function SetPrerequisite(res) {

    $(res).each(function () {
        var MainId = this.PrerequisiteId;
        $(".dvCheckBoxListControl").find("input[type=checkbox]").each(function () {
            var Id = $(this).val();
            if (MainId == Id) {
                $(this).attr('checked', true);
            }
        });
    });
}

function getManageSevices_Master(ManageSevicesMasterId) {

    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.getManageSevices_Master(ManageSevicesMasterId, ongetManageSevices_Master, null, null);
}
function ongetManageSevices_Master(result) {
    var result_ = result.split('Split_');
    var res = jQuery.parseJSON(result_[0]);
    var res_CustomerType = jQuery.parseJSON(result_[1]);
    var res_Client = jQuery.parseJSON(result_[2]);
    var res_ClientDetail = jQuery.parseJSON(result_[3]);
    var res_Product = jQuery.parseJSON(result_[4]);
    var res_Master = jQuery.parseJSON(result_[5]);
    var res_CircuitType = jQuery.parseJSON(result_[6]);
    var res_PreRequisite = jQuery.parseJSON(result_[7]);
    var res_POCDetails = jQuery.parseJSON(result_[8]);
    var CustomerEmails = jQuery.parseJSON(result_[9]);
    var CircuitTypeId = res[0].CircuitTypeId;
    var CustomerTypeId = res[0].CustomerTypeId;
    var ClientID = res[0].ClientID;
    var ClientDetailId = res[0].ClientDetailId;
    var ProductId = res[0].ProductId;
    var MasterID = res[0].MasterID;

    FillDropDownByReference(".ddlCircuitType", res_CircuitType);
    $(".ddlCircuitType").val(CircuitTypeId == null ? "0" : CircuitTypeId);
    FillDropDownByReference(".ddlCustomerType", res_CustomerType);
    $(".ddlCustomerType").val(CustomerTypeId == null ? "0" : CustomerTypeId);
    FillDropDownByReference(".ddlCustomer", res_Client);
    $(".ddlCustomer").val(ClientID == null ? "0" : ClientID);
    FillDropDownByReference(".ddlCustomerDetail", res_ClientDetail);
    $(".ddlCustomerDetail").val(ClientDetailId == null ? "0" : ClientDetailId);
    FillDropDownByReference(".ddlProduct", res_Product);
    $(".ddlProduct").val(ProductId == null ? "0" : ProductId);
    FillDropDownByReference(".ddlPrimaryIP", res_Master);
    $(".ddlPrimaryIP").val(MasterID == null ? "0" : MasterID);
    BindCheckBoxList(res_PreRequisite);
    BindPOCDetailTable(res_POCDetails);
    BindPOCEmails(CustomerEmails);
    var HasLink_ = result_[14].split(',');
    if (HasLink_[0] == "0") {
        debugger
        $(".btn_Save_Hide").hide();
        AlertBox("Alert", "Unable to Initiate Ticket because <br /> <strong> Ticket # : " + HasLink_[1] + "</strong> <br /> already opened against this link", "warning");
    }
    else {
        if (HasLink_[0] == "1") {
            debugger
            $(".btn_Save_Hide").hide();
        }

        var Exchange_POP_value = result_[10];

        if (Exchange_POP_value != "0") {
            $(".lblExchangePOP").text(Exchange_POP_value);
        }
        else {
            $(".lblExchangePOP").text("");
        }

        var SlotPOnONT = result_[11];
        if (SlotPOnONT != "0") {
            $(".DivSlotPONONT").show();
            $(".lbl_SLOT_PON_ONT").text(SlotPOnONT);
        }
        else {
            $(".lbl_SLOT_PON_ONT").text("");
            $(".DivSlotPONONT").hide();
        }

        var PortDetails = jQuery.parseJSON(result_[12]);
        if (PortDetails != "0") {
            $(".DivPortDetails").slideDown();
            BindPortDetails(PortDetails);
        }
        else {
            BindPortDetails(null);
            $(".DivPortDetails").slideUp();
        }

        if (result_[13] != "0") {
            var ServiceType_ServiceSubtype = jQuery.parseJSON(result_[13]);
            $(".lblServiceType_Master").text(ServiceType_ServiceSubtype[0].ServiceType);
            $(".lblServiceSubtype_Master").text(ServiceType_ServiceSubtype[0].ServiceSubType);
            $(".DivlblServiceType_Master").show();
            $(".DivlblServiceSubtype_Master").show();

        } else {
            $(".lblServiceType_Master").text("");
            $(".lblServiceSubtype_Master").text("");
            $(".DivlblServiceType_Master").hide();
            $(".DivlblServiceSubtype_Master").hide();
        }
    }

    HideShowPOCDiv();
    ProgressBarHide();
}

function ControlDisable() {
    $(".ddlCustomerType").attr('disabled', 'disabled');
    $(".ddlCustomer").attr('disabled', 'disabled');
    $(".ddlCustomerDetail").attr('disabled', 'disabled');
    $(".ddlProduct").attr('disabled', 'disabled');
    $(".ddlPrimaryIP").attr('disabled', 'disabled');
    $(".ddlDepartment").attr('disabled', 'disabled');
    $(".ddlLevel").attr('disabled', 'disabled');
    $(".ddlAssignee").attr('disabled', 'disabled');
    $(".ddlServiceCategory").attr('disabled', 'disabled');
    $(".ddlPriority").attr('disabled', 'disabled');
    $(".ddlRequestType").attr('disabled', 'disabled');
    $(".ddlRequestMode").attr('disabled', 'disabled');
    $(".txtTitle").attr('disabled', 'disabled');
}

function GetCircuitType() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetCircuitType(onGetCircuitType, null, null);
}
function onGetCircuitType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCircuitType", res);
    ProgressBarHide();
    // $(".ddlCircuitType").change();
}

function GetPrimaryIP(CustomerDetailId, ProductId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetPrimaryIP(CustomerDetailId, ProductId, onGetPrimaryIP, null, null);
}
function onGetPrimaryIP(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlPrimaryIP", res);
    ProgressBarHide();
    $(".ddlPrimaryIP").change();
}

//function GetInitiator() {
//    var service = new CyberTicketService.TicketService();
//    service.getInitiator(onGetInitiator, null, null);
//}
//function onGetInitiator(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlInitiator", res);
//    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
//    if (hf_TicketMasterId == "0") {
//        var UserId = $(".hf_UserId").val();
//        $(".ddlInitiator").val(UserId);
//    }

//}

function GetProducts(CustomerAddressId, CircuitTypeId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getProduct(CustomerAddressId, CircuitTypeId, onGetProducts, null, null);
}
function onGetProducts(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlProduct", res);
    ProgressBarHide();
    $(".ddlProduct").change();
}



function GetAllProducts() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetAllProducts(onGetAllProducts, null, null);
}
function onGetAllProducts(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSearchProduct", res);
    ProgressBarHide();

}


function getLevel(DepartmentId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getLevel(DepartmentId, ongetLevel, null, null);
}
function ongetLevel(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlLevel", res);
    ProgressBarHide();
}

function GetDepartments() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getDepartments(onGetDepartments, null, null);
}
function onGetDepartments(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDepartment", res);
    ProgressBarHide();
}

function GetCustomerType() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getCustomerType(onGetCustomerType, null, null);
}
function onGetCustomerType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCustomerType", res);
    ProgressBarHide();
    //$(".ddlCustomerType").change();
}

function GetCustomer(CustomerTypeId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getCustomer(CustomerTypeId, onGetCustomer, null, null);
}
function onGetCustomer(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCustomer", res);
    ProgressBarHide();
    $(".ddlCustomer").change();
}

function GetCustomerAddress(CustomerId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetCustomerAddress(CustomerId, onGetCustomerAddress, null, null);

}
function onGetCustomerAddress(result) {
    jQuery.ajaxSetup({ async: false });
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCustomerDetail", res);
    ProgressBarHide();
    $(".ddlCustomerDetail").change();
}

function GetPOCEmails(CustomerId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetPOCEmails(CustomerId, onGetPOCEmails, null, null);
}
function onGetPOCEmails(result) {
    var res = jQuery.parseJSON(result);
    BindPOCEmails(res);
    ProgressBarHide();
}

function BindPOCEmails(res) {
    $('.ListBoxCustomerSource option').each(function (index, option) {
        $(option).remove();
    });
    $('.ListBoxCustomerDestination option').each(function (index, option) {
        $(option).remove();
    });
    FillListBoxByReference(".ListBoxCustomerSource", res);
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0" && hf_TicketMasterId != null) {
    }
    else {
        $(".txtToEmail").val("");
    }
}

function GetPOCDetails(ProductId, MasterId) {
    var service = new CyberTicketService.TicketService();
    service.GetPOCDetails(ProductId, MasterId, onGetPOCDetails, null, null);
}
function onGetPOCDetails(result) {
    var res = jQuery.parseJSON(result);
    BindPOCDetailTable(res);
}

function BindPOCDetailTable(result) {
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(result).appendTo(divTbodyGoalFund);
}

function getCategory(RequestTypeId) {
    var service = new CyberTicketService.TicketService();
    service.getCategory(RequestTypeId, ongetCategory, null, null);
}
function ongetCategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCategory", res);
}

function getSubcategory(CategoryId) {

    MakeTitle();
    var service = new CyberTicketService.TicketService();
    service.getSubcategory(CategoryId, ongetSubcategory, null, null);
}
function ongetSubcategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSubcategory", res);

}


function getAssigneeByDepartmentId(DepartmentId) {
    var service = new CyberTicketService.TicketService();
    service.getAssigneeByDepartmentId(DepartmentId, ongetAssignee, null, null);
}
function ongetAssignee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAssignee", res);
}

function getPriority() {
    var service = new CyberTicketService.TicketService();
    service.getPriority(ongetPriority, null, null);
}
function ongetPriority(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlPriority", res);
}

function getCity() {
    var service = new CyberTicketService.TicketService();
    service.getCity(ongetCity, null, null);
}
function ongetCity(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCity", res);
}

function getRequestType() {
    var service = new CyberTicketService.TicketService();
    service.getRequestType(ongetRequestType, null, null);
}
function ongetRequestType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlRequestType", res);
}

function getRequestMode() {
    var service = new CyberTicketService.TicketService();
    service.getRequestMode(ongetRequestMode, null, null);
}
function ongetRequestMode(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlRequestMode", res);
    $(".ddlRequestMode").change();
}

function BindCheckBoxList(checkboxlistItems) {
    $('#dvCheckBoxListControl').html('').remove;
    var table = $('<table></table>').append($('<tr></tr>'));
    var counter = 0;
    $(checkboxlistItems).each(function () {
        table.append($('<td style="display: inline-flex;margin-left: 20px" ></td>').append($('<input>').attr({
            //  type: 'checkbox', name: 'chklistitem', value: this.Id, id: this.Id
            type: 'checkbox', name: 'chklistitem', value: this.Id, id: 'chklistitem_' + this.Id
        })).append(
            $('<label style="margin-left: 5px">').attr({
                for: 'chklistitem' + counter++
            }).text(this.Value)));
    });
    $('#dvCheckBoxListControl').append(table);
}

function FillDropDownByReference(DropDownReference, res) {
    debugger
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function FillListBoxByReference(listBoxReference, res) {
    $(listBoxReference).empty().append('');
    $(res).each(function () {
        $(listBoxReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function ClearAddCC() {
    //$(".ddlDepartmentCC").val('0');
    //$(".ddlDepartmentCC").change();
    $('.ListBoxDestination option').each(function (index, option) {
        $(option).remove();
    });


}

function ADDCC() {
    var MainResponseDesc = [];
    $('.ListBoxDestination option').each(function () {
        var Response = new Object();
        Response.User_Code = $(this).val();
        MainResponseDesc.push(Response);
    });
    ADDCCEmails($('.txtCCEmail').val().trim(), MainResponseDesc)
    $(".ddlDepartmentCC").val("0").change();
}

function ADDTo() {
    var EmailTo = $('.txtToEmail').val();
    if (EmailTo != "") {
        var lastChar = EmailTo.substr(EmailTo.length - 1); // => "1"
        if (lastChar != ";") {
            EmailTo = EmailTo + ";";
        }
    }

    $('.ListBoxCustomerDestination option').each(function () {
        var SelectedEmail = $(this).val().trim();
        if (SelectedEmail != "") {
            EmailTo = EmailTo + $(this).val() + ";";
        }
    });

    var array = EmailTo.split(';');
    var uniques = _.uniq(array);
    uniques = uniques.join(';');
    $('.txtToEmail').val(uniques)
}

function ADDCCEmails(Source, Destination) {
    var service = new CyberTicketService.TicketService();
    service.getADDCCEmails(Source, JSON.stringify(Destination), ongetADDCCEmails, null, null);
}

function ongetADDCCEmails(result) {
    $('.txtCCEmail').val(result);
}


function UploadImage() {

    ProgressBarShow();
    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {
        var UploadedFilePath = FileUpload(fileUpload);
        if (UploadedFilePath != "") {
            var hf_TicketMasterId = $(".hf_TicketMasterId").val();
            if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
                GetSaveAtachment(hf_TicketMasterId, "0", UploadedFilePath);
            }

            else {
                $(".RptAttachment").show();
                var UserId = $('.hf_UserId').val() + "00" + $('.hf_UserId').val();
                GetSaveAtachment(UserId, "0", UploadedFilePath);
            }
        }
    }
    ProgressBarHide();
}


function DeleteAtachmentByTargetID(TargetIDId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.DeleteAtachmentByTargetID(TargetIDId, onDeleteAtachmentByTargetID, null, null);
}

function onDeleteAtachmentByTargetID(result) {

}


function DeleteAtachment(FileId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.DeleteAtachment(FileId, onDeleteAtachment, null, null);
}

function onDeleteAtachment(result) {
    var res = jQuery.parseJSON(result);
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        GetSaveAtachment(hf_TicketMasterId, "0", "");
    }
    else {
        GetSaveAtachment($('.hf_UserId').val() + "00" + $('.hf_UserId').val(), "0", "");
    }
    ProgressBarHide();
}

function GetSaveAtachment(TicketMasterId, TempId, fileDetail) {

    let arr1 = fileDetail.split('||');
    if (fileDetail != "") {
        for (let i = 0; i < arr1.length; i++) {
            var service = new CyberTicketService.TicketService();
            /*  service.GetSaveAtachment(TicketMasterId, TempId, fileDetail, onGetSaveAtachment, null, null);*/
            if (arr1[i] != "")
                service.GetSaveAtachment(TicketMasterId, TempId, arr1[i], onGetSaveAtachment, null, null);
        }
    }
    else {
        var service = new CyberTicketService.TicketService();
        service.GetSaveAtachment(TicketMasterId, TempId, fileDetail, onGetSaveAtachment, null, null);
    }
}

function onGetSaveAtachment(result) {
    var res = jQuery.parseJSON(result);
    BindAttachmentRepeter(res);
}

function BindAttachmentRepeter(res) {
    var divTbodyGoalFund = $('.wfattachment').html('');
    $('#wfattachment').tmpl(res).appendTo(divTbodyGoalFund);
    $(".Attachement").val('');
}


function ValidateCircuitType() {
    var Status = false;
    if ($(".ddlProduct").val() == $(".hfEmailProductId").val()) {
        Status = true;
    }
    else {
        if ($(".ddlCircuitType").val() != "" && $(".ddlCircuitType").val() != "0" && $(".ddlCircuitType").val() != null) {
            Status = true;
        }
        else {
            Status = false;
            AlertBox("Alert", "Please select Circuit Type ", "warning");
        }
    }
    return Status;
}

//function ValidatePrimaryIP() {
//    var Status = false;
//    if ($(".ddlProduct").val() == $(".hfEmailProductId").val()) {
//        Status = true;
//    }
//    else {
//        if ($(".ddlPrimaryIP").val() != "" && $(".ddlPrimaryIP").val() != "0" && $(".ddlPrimaryIP").val() != null) {
//            Status = true;
//        }
//        else {
//            Status = false;
//            AlertBox("Alert", "Please select [CAM]-[Primary IP]-[Secondary IP]", "warning");
//        }
//    }
//    return Status;
//}



function GetChkIsRunningTicket(TicketMasterId, MasterId, DetailId) {
    $(".btn_Save_Hide").show();
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.ChkIsRunningTicket(TicketMasterId, MasterId, DetailId, onChkIsRunningTicket, null, null);
}
function onChkIsRunningTicket(result) {
    $(".btn_Save_Hide").show();
    GetServiceType_ServiceSubtype($(".ddlProduct").val(), $(".ddlPrimaryIP").val(), $(".ddlPortDetails").val());
    if (result != "") {
        debugger
        $(".btn_Save_Hide").hide();
        var HasLink_ = result.split(',');
        if (HasLink_[0] == "2") {
            AlertBox("Alert", "Unable to Initiate Ticket because <br /> <strong> Ticket # : " + HasLink_[1] + "</strong> <br /> already opened against this link", "warning");
        }
    }
    ProgressBarHide();
}


function IsTicketRunning() {
    $(".btn_Save_Hide").show();
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var ProductId = $(".ddlProduct").val() == "0" ? null : $(".ddlProduct").val();
    var MasterId = $(".ddlPrimaryIP").val() == "0" ? null : $(".ddlPrimaryIP").val();
    var DetailId = $(".ddlPortDetails").val() == "0" ? null : $(".ddlPortDetails").val();
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.ChkIsRunningTicketOnSave(hf_TicketMasterId, MasterId, DetailId, ProductId, onIsTicketRunning, null, null);
}

function onIsTicketRunning(result) {

    if (result != "") {
        ProgressBarHide();
        if (result == "1") {
            AlertBox("Alert", "Please select the ports details", "warning");
        }
        else {
            AlertBox("Alert", "Unable to Initiate Ticket because <br /> <strong> Ticket # : " + result + "</strong> <br /> already opened against this link", "warning");
        }
    }
    else {
        ProgressBarHide();
        SaveTicket();
    }
}

function PerformAction() {
    //debugger;

    if ($(".hf_TicketMasterId").val() != "0" && $(".hf_TicketMasterId").val() != "") {
        UpdateAndCreateCustomer();
    } else {
        Save();
    }

}

function Save() {

    var IsValidate = ControlsValidateFunction();

    if ($(".txtName").val() !== "" && $(".txtName").val() !== null) {

        //if ($(".txtContact").val() !== "" && $(".txtContact").val() !== null) {
        //var NumberLength = $('.txtContact').val().length;
        //if (NumberLength < 11) {
        //    $(".txtContact").css("border-color", "Red");
        //    AlertBox('Alert', 'Contact No is not valid.', 'warning');
        //    return;
        //} else { $(".txtContact").css("border-color", "#d4d4d4"); }

        //if (isContact === '1') {
        //    $(".txtContact").css("border-color", "Red");
        //    AlertBox('Alert', 'Customer information already exist against this Contact No.Please search the customer', 'warning');
        //    return;
        //}

        if ($(".txtContact").val() !== "" && $(".txtContact").val() !== null) {

            if ($(".txtAlternativeNumber").val() !== "" && $(".txtAlternativeNumber").val() !== null) {

                if ($(".txtEmail").val() !== "" && $(".txtEmail").val() !== null) {

                    var email = $('.txtEmail').val();
                    const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                    if (!re.test(email)) {
                        $(".txtEmail").css("border-color", "Red");
                        AlertBox('Alert', 'You have entered an invalid Email. Please enter valid Email in Customer Information.', 'warning');
                        return;
                    } else { $(".txtEmail").css("border-color", "#d4d4d4"); }

                    if (isEmail === '1') {
                        $(".txtEmail").css("border-color", "Red");
                        AlertBox('Alert', 'Customer information already exist against this Email.Please search the customer.', 'warning');
                        return;
                    }

                    if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0" && $(".ddlDepartment").val() != null) {
                        if ($(".ddlRequestType").val() != "" && $(".ddlRequestType").val() != "0" && $(".ddlRequestType").val() != null) {
                            if ($(".ddlCategory").val() != "" && $(".ddlCategory").val() != "0" && $(".ddlCategory").val() != null) {
                                if ($(".ddlSubcategory").val() != "" && $(".ddlSubcategory").val() != "0" && $(".ddlSubcategory").val() != null) {
                                    if ($(".ddlPriority").val() != "" && $(".ddlPriority").val() != "0" && $(".ddlPriority").val() != null) {
                                        if ($(".ddlRequestMode").val() != "" && $(".ddlRequestMode").val() != "0" && $(".ddlRequestMode").val() != null) {
                                            if ($(".txtTitle").val().trim() != "") {

                                                if ($(".txtTitle").val().length > 200) {
                                                    AlertBox('Alert', 'Title text length cannot be too long.', 'warning');
                                                    return;
                                                }

                                                var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();
                                                if (Description.length < 2000) {
                                                    if ($(".txtToEmail").val().trim() != "") {
                                                        if (IsValidate == true) {
                                                            isContact = '';
                                                            isEmail = '';
                                                            SaveTicket();
                                                        }
                                                    } else { AlertBox("Alert", "Please enter Email To", "warning"); }
                                                } else { AlertBox("Alert", "Initial finding is too long", "warning"); }
                                            } else { AlertBox("Alert", "Please enter Title", "warning"); }
                                        } else { AlertBox("Alert", "Please select Method Of Contact", "warning"); }
                                    } else { AlertBox("Alert", "Please select Priority", "warning"); }
                                } else { AlertBox("Alert", "Please select Subcategory", "warning"); }
                            } else { AlertBox("Alert", "Please select Category", "warning"); }
                        } else { AlertBox("Alert", "Please select Request Type", "warning"); }
                    } else { AlertBox("Alert", "Please select Department", "warning"); }
                } else { AlertBox("Alert", "Email field cannot be empty. Please fill out this field.", "warning"); }
            } else { AlertBox("Alert", "Alternative Number field cannot be empty. Please fill out this field.", "warning"); }
        } else { AlertBox("Alert", "Contact No field cannot be empty. Please fill out this field.", "warning"); }
    } else { AlertBox("Alert", "Name field cannot be empty. Please fill out this field.", "warning"); }
}

function SaveTicket() {

    var count = 0;
    for (var itemVal in DynamicFields) {
        if (DynamicFields[itemVal].FieldTypeId == FIELD_TYPE.TextBox) {
            if ($('.' + DynamicFields[itemVal].InputClass.trim()).val() == '') {
                count = count + 1;
            }
        }

        if (DynamicFields[itemVal].FieldTypeId == FIELD_TYPE.DropDown) {
            if ($('.' + DynamicFields[itemVal].InputClass.trim()).val() == '0' && !$('.' + DynamicFields[itemVal].InputClass.trim()).is(":disabled")) {
                count = count + 1;
            }
        }

        if (DynamicFields[itemVal].FieldTypeId == FIELD_TYPE.FileUploader) {
            if ($('.' + DynamicFields[itemVal].InputClass.trim()).val() == '') {
                count = count + 1;
            }
        }
    }

    if (count > 0) {
        AlertBox("Alert", "All Pre-Requisite fields must be filled.", "warning");
    }

    else {
        var TicketDetailData = [];
        for (var item in DynamicFields) {
            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.TextBox) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {
                    TicketDetailData.push({
                        RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                        FieldValue: $('.' + DynamicFields[item].InputClass.trim()).val(),
                        RequestTypeSubcategoryFieldTypeDetailId: null,
                    });
                }
            }

            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.DropDown) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {
                    TicketDetailData.push({
                        RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                        FieldValue: '',
                        RequestTypeSubcategoryFieldTypeDetailId: $('.' + DynamicFields[item].InputClass.trim()).val(),
                    });
                }
            }

            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.FileUploader) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {

                    var fileUpload = $('.' + DynamicFields[item].InputClass.trim()).get(0);
                    var filesCount = fileUpload.files.length;
                    if (filesCount > 0) {
                        var UploadedFilePath = FileUpload(fileUpload);
                        var result_ = UploadedFilePath.split(',');
                        var Path = result_[0];
                        TicketDetailData.push({
                            RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                            FieldValue: Path,
                            RequestTypeSubcategoryFieldTypeDetailId: null,
                        });
                    }

                }
            }


        }


        var hf_CustomerId = $(".hfTicketCustomerId").val();
        var hf_TicketMasterId = $(".hf_TicketMasterId").val();
        var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();
        var UserId = $('.hf_UserId').val();

        var Response_TicketMaster = new Object();

        var Response_Customer = new Object();

        /*   if (hf_CustomerId === "0") {*/
        Response_Customer.CustomerName = $(".txtName").val().trim();
        Response_Customer.ContactNo = $(".txtContact").val().trim();
        Response_Customer.EmailAddress = $(".txtEmail").val().trim();
        Response_Customer.CityId = $(".ddlCity").val();
        Response_Customer.Address = $(".txtAddress").val().trim();
        Response_Customer.AlternativeNumber = $(".txtAlternativeNumber").val().trim();
        //}

        Response_TicketMaster.InitiatorId = UserId;
        Response_TicketMaster.DepartmentId = $(".ddlDepartment").val() == "0" ? null : $(".ddlDepartment").val();
        Response_TicketMaster.RequestTypeId = $(".ddlRequestType").val() == "0" ? null : $(".ddlRequestType").val();
        Response_TicketMaster.RequestTypeCategoryId = $(".ddlCategory").val() == "0" ? null : $(".ddlCategory").val();
        Response_TicketMaster.RequestTypeSubCategoryId = $(".ddlSubcategory").val() == "0" ? null : $(".ddlSubcategory").val();
        Response_TicketMaster.PriorityId = $(".ddlPriority").val() == "0" ? null : $(".ddlPriority").val();
        Response_TicketMaster.RequestModeId = $(".ddlRequestMode").val() == "0" ? null : $(".ddlRequestMode").val();
        Response_TicketMaster.AssigneeId = $(".ddlAssignee").val() == "" ? null : ($(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val());
        Response_TicketMaster.StatusId = TicketStatus.New;
        Response_TicketMaster.IsAssigned = $(".ddlAssignee").val() == "" ? false : ($(".ddlAssignee").val() == "0" ? false : true);
        Response_TicketMaster.Tittle = $(".txtTitle").val().trim() == "" ? null : $(".txtTitle").val().trim();
        Response_TicketMaster.Description = Description == "" ? null : Description;

        var JSON_Response_Customer = JSON.stringify(Response_Customer);
        var JSON_Response_TicketMaster = JSON.stringify(Response_TicketMaster);
        var TicketDetailDatajson = JSON.stringify(TicketDetailData);


        var FilePath = "";
        if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        }
        else {
            var fileUpload = $('.Attachement').get(0);
            var filesCount = fileUpload.files.length;
            if (filesCount > 0) {
                FilePath = FileUpload(fileUpload);
            }
        }


        ProgressBarShow();
        var service = new CyberTicketService.TicketService();
        service.SaveTicket(hf_CustomerId, hf_TicketMasterId, JSON_Response_Customer, JSON_Response_TicketMaster, $(".txtToEmail").val().trim(), $(".txtCCEmail").val().trim(), FilePath, TicketDetailDatajson, onSaveTicket, null, null);

    }

}

function onSaveTicket(result) {
    ProgressBarHide();
    if (result != "") {
        $('.hf_TicketMasterId').val("0");
        AlertBoxSave("Success!", "Ticket has been Initiated successfully Ticket No :  " + result + "" + "", "success");

    } else
        AlertBox("Alert", "Unable to Initiate Ticket", "warning");
}

function Update() {
    var IsValidate = ControlsValidateFunction();

    if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0" && $(".ddlDepartment").val() != null) {
        if ($(".ddlRequestType").val() != "" && $(".ddlRequestType").val() != "0" && $(".ddlRequestType").val() != null) {
            if ($(".ddlCategory").val() != "" && $(".ddlCategory").val() != "0" && $(".ddlCategory").val() != null) {
                if ($(".ddlSubcategory").val() != "" && $(".ddlSubcategory").val() != "0" && $(".ddlSubcategory").val() != null) {
                    if ($(".ddlPriority").val() != "" && $(".ddlPriority").val() != "0" && $(".ddlPriority").val() != null) {
                        if ($(".ddlRequestMode").val() != "" && $(".ddlRequestMode").val() != "0" && $(".ddlRequestMode").val() != null) {
                            if ($(".txtTitle").val().trim() != "") {

                                if ($(".txtTitle").val().length > 200) {
                                    AlertBox('Alert', 'Title text length cannot be too long.', 'warning');
                                    return;
                                }

                                if ($(".txtToEmail").val().trim() != "") {
                                    if (IsValidate == true) {

                                        UpdateTicket();
                                    }
                                } else { AlertBox("Alert", "Please enter Email To", "warning"); }
                            } else { AlertBox("Alert", "Please enter Title", "warning"); }
                        } else { AlertBox("Alert", "Please select Method Of Contact", "warning"); }
                    } else { AlertBox("Alert", "Please select Priority", "warning"); }
                } else { AlertBox("Alert", "Please select Subcategory", "warning"); }
            } else { AlertBox("Alert", "Please select Category", "warning"); }
        } else { AlertBox("Alert", "Please select Request Type", "warning"); }
    } else { AlertBox("Alert", "Please select Department", "warning"); }

}

function UpdateTicket() {
    // var hfTicketCustomerId = $(".hfTicketCustomerId").val();

    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var InitiatorId = $(".hfInitiatorId").val();

    var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();

    var Response_TicketMaster = new Object();

    Response_TicketMaster.InitiatorId = InitiatorId;
    Response_TicketMaster.DepartmentId = $(".ddlDepartment").val() == "0" ? null : $(".ddlDepartment").val();
    Response_TicketMaster.RequestTypeId = $(".ddlRequestType").val() == "0" ? null : $(".ddlRequestType").val();
    Response_TicketMaster.RequestTypeCategoryId = $(".ddlCategory").val() == "0" ? null : $(".ddlCategory").val();
    Response_TicketMaster.RequestTypeSubCategoryId = $(".ddlSubcategory").val() == "0" ? null : $(".ddlSubcategory").val();
    Response_TicketMaster.PriorityId = $(".ddlPriority").val() == "0" ? null : $(".ddlPriority").val();
    Response_TicketMaster.RequestModeId = $(".ddlRequestMode").val() == "0" ? null : $(".ddlRequestMode").val();
    Response_TicketMaster.AssigneeId = $(".ddlAssignee").val() == "" ? null : ($(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val());
    Response_TicketMaster.StatusId = TicketStatus.New;


    Response_TicketMaster.IsAssigned = $(".ddlAssignee").val() == "" ? false : ($(".ddlAssignee").val() == "0" ? false : true);
    Response_TicketMaster.Tittle = $(".txtTitle").val().trim() == "" ? null : $(".txtTitle").val().trim();
    Response_TicketMaster.Description = Description == "" ? null : Description;

    var JSON_Response_TicketMaster = JSON.stringify(Response_TicketMaster);

    var FilePath = "";

    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {
        FilePath = FileUpload(fileUpload);
    }

    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.UpdateTicket(hf_TicketMasterId, JSON_Response_TicketMaster, $(".txtToEmail").val().trim(), $(".txtCCEmail").val().trim(), FilePath, onUpdateTicket, null, null);
}

function onUpdateTicket(result) {
    ProgressBarHide();
    if (result != "") {
        $('.hf_TicketMasterId').val("0");
        AlertBoxSave("Success!", "Ticket has been updated successfully Ticket No :  " + result + "" + "", "success");
    } else
        AlertBox("Alert", "Unable to Initiate Ticket", "warning");
}

function UpdateAndCreateCustomer() {

    var IsValidate = ControlsValidateFunction();

    if ($(".txtName").val() !== "" && $(".txtName").val() !== null) {

        //if ($(".txtContact").val() !== "" && $(".txtContact").val() !== null) {
        //var NumberLength = $('.txtContact').val().length;
        //if (NumberLength < 11) {
        //    $(".txtContact").css("border-color", "Red");
        //    AlertBox('Alert', 'Contact No is not valid.', 'warning');
        //    return;
        //} else { $(".txtContact").css("border-color", "#d4d4d4"); }

        //if (isContact === '1') {
        //    $(".txtContact").css("border-color", "Red");
        //    AlertBox('Alert', 'Customer information already exist against this Contact No.Please search the customer.', 'warning');
        //    return;
        //}

        if ($(".txtEmail").val() !== "" && $(".txtEmail").val() !== null) {

            var email = $('.txtEmail').val();
            const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            if (!re.test(email)) {
                $(".txtEmail").css("border-color", "Red");
                AlertBox('Alert', 'You have entered an invalid Email. Please enter valid Email in Customer Information.', 'warning');
                return;
            } else { $(".txtEmail").css("border-color", "#d4d4d4"); }

            //if (isEmail === '1') {
            //    $(".txtEmail").css("border-color", "Red");
            //    AlertBox('Alert', 'Customer information already exist against this Email.Please search the customer.', 'warning');
            //    return;
            //}

            if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0" && $(".ddlDepartment").val() != null) {
                if ($(".ddlRequestType").val() != "" && $(".ddlRequestType").val() != "0" && $(".ddlRequestType").val() != null) {
                    if ($(".ddlCategory").val() != "" && $(".ddlCategory").val() != "0" && $(".ddlCategory").val() != null) {
                        if ($(".ddlSubcategory").val() != "" && $(".ddlSubcategory").val() != "0" && $(".ddlSubcategory").val() != null) {
                            if ($(".ddlPriority").val() != "" && $(".ddlPriority").val() != "0" && $(".ddlPriority").val() != null) {
                                if ($(".ddlRequestMode").val() != "" && $(".ddlRequestMode").val() != "0" && $(".ddlRequestMode").val() != null) {
                                    if ($(".txtTitle").val().trim() != "") {

                                        if ($(".txtTitle").val().length > 200) {
                                            AlertBox('Alert', 'Title text length cannot be too long.', 'warning');
                                            return;
                                        }

                                        //var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();
                                        //if (Description != "") {

                                        if ($(".txtToEmail").val().trim() != "") {
                                            if (IsValidate == true) {
                                                isContact = '';
                                                isEmail = '';
                                                UpdateTicketAndCreateCustomer();
                                            }
                                        } else { AlertBox("Alert", "Please enter Email To", "warning"); }

                                        //} else { AlertBox("Alert", "Please enter Initial findings", "warning"); }
                                    } else { AlertBox("Alert", "Please enter Title", "warning"); }
                                } else { AlertBox("Alert", "Please select Method Of Contact", "warning"); }
                            } else { AlertBox("Alert", "Please select Priority", "warning"); }
                        } else { AlertBox("Alert", "Please select Subcategory", "warning"); }
                    } else { AlertBox("Alert", "Please select Category", "warning"); }
                } else { AlertBox("Alert", "Please select Request Type", "warning"); }
            } else { AlertBox("Alert", "Please select Department", "warning"); }
        } else { AlertBox("Alert", "Email field cannot be empty. Please fill out this field.", "warning"); }
        //} else { AlertBox("Alert", "Contact No field cannot be empty. Please fill out this field.", "warning"); }
    } else { AlertBox("Alert", "Name field cannot be empty. Please fill out this field.", "warning"); }
}

function UpdateTicketAndCreateCustomer() {

    var count = 0;
    for (var itemVal in DynamicFields) {
        if (DynamicFields[itemVal].FieldTypeId == FIELD_TYPE.TextBox) {
            if ($('.' + DynamicFields[itemVal].InputClass.trim()).val() == '') {
                count = count + 1;
            }
        }

        if (DynamicFields[itemVal].FieldTypeId == FIELD_TYPE.DropDown) {
            if ($('.' + DynamicFields[itemVal].InputClass.trim()).val() == '0' && !$('.' + DynamicFields[itemVal].InputClass.trim()).is(":disabled")) {
                count = count + 1;
            }
        }


    }

    if (count > 0) {
        AlertBox("Alert", "All Pre-Requisite fields must be filled.", "warning");
    }
    else {
        var TicketDetailData = [];
        for (var item in DynamicFields) {
            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.TextBox) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {
                    TicketDetailData.push({
                        RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                        FieldValue: $('.' + DynamicFields[item].InputClass.trim()).val(),
                        RequestTypeSubcategoryFieldTypeDetailId: null,
                    });
                }
            }

            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.DropDown) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {
                    TicketDetailData.push({
                        RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                        FieldValue: '',
                        RequestTypeSubcategoryFieldTypeDetailId: $('.' + DynamicFields[item].InputClass.trim()).val(),
                    });
                }
            }

            if (DynamicFields[item].FieldTypeId == FIELD_TYPE.FileUploader) {
                if ($('.' + DynamicFields[item].InputClass.trim()).val() != '') {

                    var fileUpload = $('.' + DynamicFields[item].InputClass.trim()).get(0);
                    var filesCount = fileUpload.files.length;
                    if (filesCount > 0) {

                        var UploadedFilePath = FileUpload(fileUpload);
                        var result_ = UploadedFilePath.split(',');
                        var Path = result_[0];
                        TicketDetailData.push({
                            RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                            FieldValue: Path,
                            RequestTypeSubcategoryFieldTypeDetailId: null,
                        });
                    }

                }


                else {

                    var Path = ArrayImgURL[0];
                    TicketDetailData.push({
                        RequestTypeSubcategoryFieldTypeId: DynamicFields[item].RequestTypeSubcategoryFieldTypeId,
                        FieldValue: Path,
                        RequestTypeSubcategoryFieldTypeDetailId: null,
                    });
                }
            }


        }

        var TicketDetailDatajson = JSON.stringify(TicketDetailData);
        var hf_CustomerId = $(".hfTicketCustomerId").val();
        var checkBox = document.getElementById('chkNewCustomer');
        var Response_Customer = new Object();

        if (checkBox.checked) {
            hf_CustomerId = "0";
            Response_Customer.CustomerName = $(".txtName").val().trim();
            Response_Customer.ContactNo = $(".txtContact").val().trim();
            Response_Customer.EmailAddress = $(".txtEmail").val().trim();
            Response_Customer.CityId = $(".ddlCity").val();
            Response_Customer.Address = $(".txtAddress").val().trim();
            Response_Customer.AlternativeNumber = $(".txtAlternativeNumber").val().trim();
        }
        else {
            Response_Customer.CustomerName = $(".txtName").val().trim();
            Response_Customer.ContactNo = $(".txtContact").val().trim();
            Response_Customer.EmailAddress = $(".txtEmail").val().trim();
            Response_Customer.CityId = $(".ddlCity").val();
            Response_Customer.Address = $(".txtAddress").val().trim();
            Response_Customer.AlternativeNumber = $(".txtAlternativeNumber").val().trim();
        }

        var hf_TicketMasterId = $(".hf_TicketMasterId").val();
        var UserId = $('.hf_UserId').val();
        var InitiatorId = $(".hfInitiatorId").val();
        var hfStatusId = $(".hfStatusId").val();

        var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();

        var Response_TicketMaster = new Object();

        if (InitiatorId === "") {
            Response_TicketMaster.InitiatorId = UserId;
        } else {
            Response_TicketMaster.InitiatorId = InitiatorId;
        }

        Response_TicketMaster.DepartmentId = $(".ddlDepartment").val() == "0" ? null : $(".ddlDepartment").val();
        Response_TicketMaster.RequestTypeId = $(".ddlRequestType").val() == "0" ? null : $(".ddlRequestType").val();
        Response_TicketMaster.RequestTypeCategoryId = $(".ddlCategory").val() == "0" ? null : $(".ddlCategory").val();
        Response_TicketMaster.RequestTypeSubCategoryId = $(".ddlSubcategory").val() == "0" ? null : $(".ddlSubcategory").val();
        Response_TicketMaster.PriorityId = $(".ddlPriority").val() == "0" ? null : $(".ddlPriority").val();
        Response_TicketMaster.RequestModeId = $(".ddlRequestMode").val() == "0" ? null : $(".ddlRequestMode").val();
        Response_TicketMaster.AssigneeId = $(".ddlAssignee").val() == "" ? null : ($(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val());
        Response_TicketMaster.StatusId = TicketStatus.New;

        Response_TicketMaster.IsAssigned = $(".ddlAssignee").val() == "" ? false : ($(".ddlAssignee").val() == "0" ? false : true);
        Response_TicketMaster.Tittle = $(".txtTitle").val().trim() == "" ? null : $(".txtTitle").val().trim();
        Response_TicketMaster.Description = Description == "" ? null : Description;

        var JSON_Response_Customer = JSON.stringify(Response_Customer);
        var JSON_Response_TicketMaster = JSON.stringify(Response_TicketMaster);

        var FilePath = "";

        var fileUpload = $('.Attachement').get(0);
        var filesCount = fileUpload.files.length;
        if (filesCount > 0) {
            FilePath = FileUpload(fileUpload);
        }

        ProgressBarShow();
        var service = new CyberTicketService.TicketService();
        service.UpdateTicketAndCreateCustomer(hf_CustomerId, hf_TicketMasterId, JSON_Response_Customer, JSON_Response_TicketMaster, $(".txtToEmail").val().trim(), $(".txtCCEmail").val().trim(), FilePath, hfStatusId, TicketDetailDatajson, onUpdateTicketAndCreateCustomer, null, null);

    }
}

function onUpdateTicketAndCreateCustomer(result) {
    ProgressBarHide();
    //debugger;
    var res = jQuery.parseJSON(result);
    if (res.length == 1) {
        AlertBoxSave("Success!", "Ticket has been updated successfully <br> Ticket No :  " + res[0] + "", "success");
    } else if (res.length == 2) {
        AlertBoxSave("Success!", "Ticket status has been already updated by another user. <br><br> Ticket No :  " + res[0] + "" + " <br><br> Status : " + res[1] + "", "success");
    } else {
        AlertBox("Alert", "Unable to Update Ticket", "warning");
    }

}

function AlertBox(title, Message, type) {
    //swal(title, Message, type);
    swal({
        title: title,
        text: Message,
        type: type,
        html: true
    })
}

function AlertBoxSave(title, Message, type, Page) {

    swal({
        title: title,
        text: Message,
        type: type,
        html: true
    },
        function () {
            var page = '/Pages/Tickets.aspx';
            window.location.href = page;
        });

}


function getTicketCC(TicketMasterId) {
    var service = new CyberTicketService.TicketService();
    service.getTicketCC(TicketMasterId, ongetTicketCC, null, null);
}
function ongetTicketCC(result) {
    var res = jQuery.parseJSON(result);
    $('.txtCCEmail').val(res);
}


function getTicketTo(TicketMasterId) {
    var service = new CyberTicketService.TicketService();
    service.getTicketTo(TicketMasterId, ongetTicketTo, null, null);
}
function ongetTicketTo(result) {
    var res = jQuery.parseJSON(result);
    $('.txtToEmail').val(res);

}



function SleepMethod(SleepTime) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.SleepMethod(SleepTime, onSleepMethod, null, null);
}
function onSleepMethod() {
    ProgressBarHide();
}



function TestEmail() {

    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var service = new CyberTicketService.TicketService();
    service.TestEmail(hf_TicketMasterId, $(".txtToEmail").val().trim(), $(".txtCCEmail").val().trim(), onTestEmail, null, null);
    //ProgressBarShow();
}
function onTestEmail(result) {

}


function OnbtnCustomerMoveRight() {
    $('.ListBoxCustomerSource :selected').each(function (i, selected) {
        $('.ListBoxCustomerDestination').append($("<option></option>").val($(this).val()).html($(this).val()));
    });
    $(".ListBoxCustomerSource option:selected").each(function () {
        $(this).remove(); //or whatever else
    });
}
function OnbtnCustomerMoveLeft() {

    $('.ListBoxCustomerDestination :selected').each(function (i, selected) {
        $('.ListBoxCustomerSource').append($("<option></option>").val($(this).val()).html($(this).val()));
    });
    $(".ListBoxCustomerDestination option:selected").each(function () {
        $(this).remove(); //or whatever else
    });

}


function IsValidPOCEmail() {

    var status = true;
    //var RequestMode = $(".ddlRequestMode").val()
    //if (RequestMode == "3") {
    //    $(".tr_").each(function () {
    //        var Email = $(this).find(".POCEmail").val();
    //        if (Email != "") {
    //            if (validateEmail(Email) == false) {
    //                if (status == true) {
    //                    status == false
    //                }
    //            }
    //        }
    //    })
    //}
    return status;
}
function validateEmail(field) {
    var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
    return (regex.test(field)) ? true : false;
}


function SearchCDMSLink() {
    ProgressBarShow();
    var ProductId = $(".ddlSearchProduct").val() == "" ? null : ($(".ddlSearchProduct").val() == "0" ? null : $(".ddlSearchProduct").val());
    var RegionId = $(".ddlSearchRegion").val() == "" ? null : ($(".ddlSearchRegion").val() == "0" ? null : $(".ddlSearchRegion").val());
    var CityId = $(".ddlSearchCity").val() == "" ? null : ($(".ddlSearchCity").val() == "0" ? null : $(".ddlSearchCity").val());
    var ExchangePOPId = $(".ddlSearchExchangePOP").val() == "" ? null : ($(".ddlSearchExchangePOP").val() == "0" ? null : $(".ddlSearchExchangePOP").val());
    var StatusId = $(".ddSearchlStatus").val() == "" ? null : ($(".ddSearchlStatus").val() == "0" ? null : $(".ddSearchlStatus").val());
    var CircuitTypeId = $(".ddlSearchCircuitType").val() == "" ? null : ($(".ddlSearchCircuitType").val() == "0" ? null : $(".ddlSearchCircuitType").val());
    var Customer = $(".txtSearchCustomer").val().trim() == "" ? null : $(".txtSearchCustomer").val().trim();
    var Address = $(".txtSearchAddress").val().trim() == "" ? null : $(".txtSearchAddress").val().trim();
    var CAM = $(".txtSearchCAM").val().trim() == "" ? null : $(".txtSearchCAM").val().trim();
    var IPAddress = $(".txtSearchIPAddress").val().trim() == "" ? null : $(".txtSearchIPAddress").val().trim();
    var BranchCode = $(".txtSearchBranchCode").val().trim() == "" ? null : $(".txtSearchBranchCode").val().trim();
    var service = new CyberTicketService.TicketService();
    service.GetGlobelSerach(ProductId, RegionId, CityId, ExchangePOPId, Customer, null, null, $(".hfPhoneNumber").val().trim(), CAM, IPAddress, BranchCode, Address, StatusId, CircuitTypeId, ongetSearchCDMSLink, null, null);
}

function ongetSearchCDMSLink(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfCDMSLinks').html('');
    $('#wfCDMSLinks').tmpl(res).appendTo(divTbodyGoalFund);
    $('.tr').each(function () {
        var d = $(this).find(".HfRunningTicketCountAgainstLink").val();
        if (d > 0) {
            //$('.buttonTicket').hide();
            $(this).find('.buttonTicket').hide();
        }
        else {

        }
    });


    $(".buttonTicket").click(function () {
        CloseSearchPopupModal();
        var hf_ManageSevicesMasterId = $(this).closest("tr").find(".HfManageSevicesMasterId").val()
        getManageSevices_Master(hf_ManageSevicesMasterId);
        return false;
    });

    ProgressBarHide();
}


function ConfirmBox(title, Message, type) {
    var IsYes = false;
    swal({
        title: title,
        text: Message,
        type: type,
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                IsYes = true;
            } else {
                IsYes = false;
            }
        });
    return IsYes;
}


function SetDynamicFields(TicketSubCategoryId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.SetDynamicFields(TicketSubCategoryId, onSetDynamicFields, null, null);

}
function onSetDynamicFields(result) {
    if (result != "") {
        if (result.Response === "Success") {
            var res = result.Data;
            ////Console.log("Dynamic");
            ////Console.log(res);
            GenerateDivBody(".dynamicFields", res);
        }
    }
    ProgressBarHide();
}

function GenerateDivBody(rowClassPrefix, data) {
    ProgressBarHide();
    debugger
    let GenerateDiv = '';
    // Generate Div
    ////Console.log("edfsdf" + data["Table"]);


    var val = 0;
    for (var item in data["Table"]) {

        var DynamicDiv = "";
        if (val == 4) {
            val = 0;
            DynamicDiv += '<div class="controls-row">'
        }

        DynamicDiv += '<div class="control-group span3">';
        //DynamicDiv += '<input type="hidden" class="hfTicketTypeId" value="' + data["Table"][item].TicketTypeId + '" />';
        //DynamicDiv += '<input type="hidden" class="hfTicketWorkCodeId" value="' + data["Table"][item].TicketWorkCodeId + '" />';
        DynamicDiv += '<input type="hidden" class="hfFieldTypeId" value="' + data["Table"][item].FieldTypeId + '" />';
        var FieldType_Id = data["Table"][item].FieldTypeId;

        if (FieldType_Id == FIELD_TYPE.TextBox) {
            DynamicDiv += '<label class="label-bold">' + data["Table"][item].Caption + '</label>';
            DynamicDiv += '<div class="controls">';
            DynamicDiv += '<input type="text" onkeypress="return Validate(event);" class="span12 textbox_ ' + data["Table"][item].InputClass + '"/>';
            DynamicDiv += '</div>';
            val = val + 1;
        }

        if (FieldType_Id == FIELD_TYPE.DropDown) {

            DynamicDiv += '<label class="label-bold">' + data["Table"][item].Caption + '</label>';
            DynamicDiv += '<div class="controls">';
            DynamicDiv += '<select type="text"  class="span12 dropDown_ ' + data["Table"][item].InputClass + '"> </select>';
            DynamicDiv += '</div>';
            val = val + 1;
        }

        if (FieldType_Id == FIELD_TYPE.FileUploader) {

            DynamicDiv += '<label class="label-bold">' + data["Table"][item].Caption + '</label>';
            DynamicDiv += '<div class="controls">';
            DynamicDiv += '<input  type="file"  class="span12 File_ ' + data["Table"][item].InputClass + '"/>';
            DynamicDiv += '</div>';
            val = val + 1;
        }

        DynamicDiv += '</div>';
        GenerateDiv += DynamicDiv;
    }
    $(rowClassPrefix).html(GenerateDiv);
    DynamicFields = data.Table;

    for (var item in data["Table"]) {
        var FieldType_Id = data["Table"][item].FieldTypeId;
        if (FieldType_Id == FIELD_TYPE.DropDown) {

            var RequestTypeSubcategoryFieldTypeId = data["Table"][item].RequestTypeSubcategoryFieldTypeId;
            //var InputClass = data["Table"][item].InputClass;

            //if (InputClass == "ReportedBy") {
            //    var DropDownValues = $.grep(data["Table2"], function (v) {
            //        return v;
            //    });
            //}
            //else {
            //    var DropDownValues = $.grep(data["Table1"], function (v) {
            //        return v.RequestTypeSubcategoryFieldTypeId === RequestTypeSubcategoryFieldTypeId;
            //    });
            //}

            ////Console.log("Names");
            ////Console.log(DropDownValues);

            var DropDownValues = $.grep(data["Table1"], function (v) {
                return v.RequestTypeSubcategoryFieldTypeId === RequestTypeSubcategoryFieldTypeId;
            });
            debugger
            var classrrs = "." + data["Table"][item].InputClass + "";
            if ($(classrrs.toString()).length) {
                FillDropDownByReference(classrrs.toString(), DropDownValues);
            }

            if ($('.ReportedBy :selected').text() == "Bank") {   // Bank
                $('#dynamicFields').find('.Bank').removeAttr('disabled');
            }
            else {
                //$('#dynamicFields').find('.Bank').val("0");
                //$('#dynamicFields').find('.Bank').attr('disabled', 'disabled');
            }


            if ($('.ReportedBy :selected').text() == "OGA") {   // OGA
                $('#dynamicFields').find('.OGA').removeAttr('disabled');
                $('#dynamicFields').find('.OGA2').removeAttr('disabled');
            }
            else {
                //$('#dynamicFields').find('.OGA').val("0");
                //$('#dynamicFields').find('.OGA').attr('disabled', 'disabled');

                //$('#dynamicFields').find('.OGA2').val("0");
                //$('#dynamicFields').find('.OGA2').attr('disabled', 'disabled');
            }
        }
    }
    if ($(".hf_TicketMasterId").val() != "0" && $(".hf_TicketMasterId").val() != "") {
        SetDynamicFieldsOnEdit($(".hf_TicketMasterId").val());
    }
    ProgressBarHide();
}

function Validate(e) {
    var keyCode = e.keyCode || e.which;

    //Regex to allow only Alphabets Numbers Dash Underscore and Space
    var pattern = /^[a-z\d\-_\s]+$/i;

    //Validating the textBox value against our regex pattern.
    var isValid = pattern.test(String.fromCharCode(keyCode));
    return isValid;
}


function SetDynamicFieldsOnEdit(TicketMasterId) {
    var service = new CyberTicketService.TicketService();
    service.getTicketSubCategoryDetal_DataOnEdit(TicketMasterId, onSetDynamicFieldsOnEdit, null, null);
}

function onSetDynamicFieldsOnEdit(result) {
    if (result != "") {
        if (result.Response === "Success") {
            var res = result.Data;
            ////Console.log(res);
            ////Console.log(res.Table2)
            // GenerateDivBodyOnEdit(".dynamicFields", res.Table1, res.Table2);
            setDataOnEdit(res.Table, res.Table1);
            // DisableControlsOnStatus(res.Table[0].TicketStatusId);
        }
    }
}

function setDataOnEdit(data, data2) {
    for (var item in data) {

        //////Console.log("Start of Data");
        var SubcategoryId = data[item].RequestTypeSubcategoryId;

        var FieldTypeId = data[item].FieldTypeId;
        var classrrs = "." + data[item].InputClass + "";
        if (SubcategoryId == $('.ddlSubcategory').val()) {
            if (FieldTypeId == FIELD_TYPE.DropDown) {

                var RequestTypeSubcategoryFieldTypeId = data[item].RequestTypeSubcategoryFieldTypeId;
                //var InputClass = data[item].InputClass;

                //if (InputClass == "ReportedBy") {
                //    var DropDownValues = $.grep(data3, function (v) {
                //        return v;
                //    });
                //}
                //else {
                //    var DropDownValues = $.grep(data2, function (v) {
                //        return v.RequestTypeSubcategoryFieldTypeId === RequestTypeSubcategoryFieldTypeId;
                //    });
                //}


                ////Console.log("Names");
                ////Console.log(DropDownValues);

                var DropDownValues = $.grep(data2, function (v) {
                    return v.RequestTypeSubcategoryFieldTypeId === RequestTypeSubcategoryFieldTypeId;
                });

                if ($(classrrs.toString()).length) {
                    FillDropDownByReference(classrrs.toString(), DropDownValues);
                }

                $(classrrs.toString()).val(data[item].RequestTypeSubcategoryFieldTypeDetailId);


                if ($('.ReportedBy :selected').text() == "Bank") {   // Bank
                    $('#dynamicFields').find('.Bank').removeAttr('disabled');
                }
                else {
                    //$('#dynamicFields').find('.Bank').val("0");
                    //$('#dynamicFields').find('.Bank').attr('disabled', 'disabled');
                }

                if ($('.ReportedBy :selected').text() == "OGA") {   // OGA
                    $('#dynamicFields').find('.OGA').removeAttr('disabled');
                    $('#dynamicFields').find('.OGA2').removeAttr('disabled');
                }
                else {
                    //$('#dynamicFields').find('.OGA').val("0");
                    //$('#dynamicFields').find('.OGA').attr('disabled', 'disabled');

                    //$('#dynamicFields').find('.OGA2').val("0");
                    //$('#dynamicFields').find('.OGA2').attr('disabled', 'disabled');
                }

            }
            else if (FieldTypeId == FIELD_TYPE.TextBox) {

                $(classrrs.toString()).val(data[item].FieldValue);
            }

            else if (FieldTypeId == FIELD_TYPE.FileUploader) {

                $(".div_Attachment").show();
                var path = "/Uploads/" + data[item].FieldValue;
                var ImagePath = document.getElementsByClassName("FileImageRef");
                ImagePath[0].setAttribute('href', path);
                ArrayImgURL.push(data[item].FieldValue);
            }

        }

    }
}





