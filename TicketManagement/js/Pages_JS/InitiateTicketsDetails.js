//ProgressBarShow();
//ProgressBarHide();

var CurrentStatusId = 0;
var FaultId = 0;

var Role =
{
    Admin: "19",
    Level_1: "20",
    Level_2_3: "28"
}

var TicketStatus =
{
    InProgress: 1,
    Cancel: 2,
    Closed: 3,
    Escalated: 4,
    Resolved: 5,
    OnHold: 6,
    ReOpen: 7,
    Junk: 8,
}
function OnLoad() {
    $('.TypeOfComplaint').hide();
    $('.pnlCustomer').hide();
    $('.btnCancelSearchCustomer').hide();
    $('.txtSearchContact').val("");
    $('.txtSearchEmail').val("");
    $('.txtName').prop('disabled', true);
    $('.txtContact').prop('disabled', true);
    $('.txtEmail').prop('disabled', true);
    $(".ddlDepartment").change(function () {
        //GetServiceCategory($(this).val());
        getAssigneeByDepartmentId($(this).val(), 0);
    })
    $(".div_Desc").hide();
    $(".ddlTypeOfIssue").change(function () {
        getProductSubCategoryByProductId($(this).val());
    })

    $(".ddlStatus").change(function () {

        var id = $(this).val();

        if (id === TicketStatus.Closed.toString()) {
            GetTypeOfComplaint();
            $('.TypeOfComplaint').show();
        } else {
            $('.TypeOfComplaint').hide();
        }

        $(".txtDescription").val('');
        $(".div_Desc").slideUp();

    })

    $(".ddlRequestType").change(function () {
        getCategory($(".ddlRequestType").val(), $(this).val());
        getSubcategory("0", $(this).val());
    });

    $(".ddlCategory").change(function () {
        getSubcategory($(".ddlCategory").val(), $(this).val());
    });

    $(".btnCancelSearchCustomer").click(function () {
        $('.txtSearchContact').val("");
        $('.txtSearchEmail').val("");
        $('.txtName').val("");
        $('.txtContact').val("");
        $('.txtEmail').val("");
        $('.btnCancelSearchCustomer').hide();
        $('.pnlCustomer').hide();
    });


    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "0" && hf_TicketMasterId != "") {
        getTicketMasterDetails(hf_TicketMasterId);
        GetSaveAtachment(hf_TicketMasterId, "0", "");
    }




}

function getProductSubCategoryByProductId(ProductId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getProductSubCategoryByProductId(ProductId, ongetProductSubCategoryByProductId, null, null);
}
function ongetProductSubCategoryByProductId(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlProductSubCategory", res);
    if (res.length == 1) {
        $(".ddlProductSubCategory").val(res[0].Id);
    }
    else {
        $(".ddlProductSubCategory").val($(".hf_ProductSubCategoryId").val());
    }
    ProgressBarHide();
}





//function GetAssignee() {
//    var service = new CyberTicketService.TicketService();
//    service.getInitiator(onGetAssignee, null, null);
//}
//function onGetAssignee(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlAssignee", res);
//}

function getAssigneeByDepartmentId(DepartmentId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getAssigneeByDepartmentId(DepartmentId, ongetAssignee, null, null);
}
function ongetAssignee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAssignee", res);
    $(".ddlAssignee").val($(".hf_AssigneeId").val());
    ProgressBarHide();
}

function UploadImage() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {
        var UploadedFilePath = FileUpload(fileUpload);
        if (UploadedFilePath != "") {
            if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
                GetSaveAtachment(hf_TicketMasterId, "0", UploadedFilePath);
            }
        }
    }
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

function GetSaveAtachment(TicketMasterId, TempId, fileDetail) {
    var service = new CyberTicketService.TicketService();
    service.GetSaveAtachment(TicketMasterId, TempId, fileDetail, onGetSaveAtachment, null, null);
}
function onGetSaveAtachment(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfattachment').html('');
    $('#wfattachment').tmpl(res).appendTo(divTbodyGoalFund);
    $(".Attachement").val('');

}




function GetTypeOfIssue() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetTypeOfIssue(OnGetTypeOfIssue, null, null);
}
function OnGetTypeOfIssue(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTypeOfIssue", res);
    $(".ddlTypeOfIssue").val($(".hf_TypeOfIssueId").val()).change();

    ProgressBarHide();
}

function GetTypeOfComplaint() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetTypeOfComplaint(OnGetTypeOfComplaint, null, null);
}
function OnGetTypeOfComplaint(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTypeOfComplaint", res);
    ProgressBarHide();
}

function getTicketMasterDetails(TicketMasterId) {
    ProgressBarShow();
    
    var service = new CyberTicketService.TicketService();
    service.getTicketMasterDetails(TicketMasterId, ongetTicketMasterDetails, null, null);
}

function ongetTicketMasterDetails(result) {
    if (result != "") {
        debugger;
        var RoleCode = $('.hf_RoleCode').val();
        var Result_ = result.split('Split_');
        var res = jQuery.parseJSON(Result_[0]);
        var StatusId = res[0].StatusId == null ? "0" : res[0].StatusId;
        CurrentStatusId = StatusId;
        var hf_UserId = $(".hf_UserId").val();
        var hf_IsSuperAdmin = $(".hf_IsSuperAdmin").val();
        var hf_IsAdmin = $(".hf_IsAdmin").val();
        var hf_DepartmentId = $(".hf_DepartmentId").val();
        var AssigneeId = res[0].AssigneeId == null ? "0" : res[0].AssigneeId;
        var InitiatorId = res[0].InitiatorId == null ? "0" : res[0].InitiatorId;
        var TypeOfIssueId = res[0].TypeOfIssueId == null ? "0" : res[0].TypeOfIssueId;
        var ProductSubCategoryId = res[0].ProductSubCategoryId == null ? "0" : res[0].ProductSubCategoryId;
        $(".hf_AssigneeId").val(AssigneeId);
        $(".hf_StatusId").val(StatusId);
        $(".hf_TypeOfIssueId").val(TypeOfIssueId);
        $(".hf_ProductSubCategoryId").val(ProductSubCategoryId);
        $(".lblName").text(res[0].CustomerName);
        $(".lblContact").text(res[0].CustomerContact);
        $(".lblEmail").text(res[0].CustomerEmail);
        $(".lblCity").text(res[0].CustomerCity);
        $(".lblAddress").text(res[0].CustomerAddress);
        $(".lblAltNumber").text(res[0].CustomerAlternativeNumber);
        $(".lblStatus").text(res[0].Status);
        $(".lblTicketNo").text(res[0].TicketCode);
        $(".lblInitiator").text(res[0].Initiator);
        $(".lbl_Department").text(res[0].Department == null ? "" : res[0].Department);
        $(".lbl_Category").text(res[0].CategoryName == null ? "" : res[0].CategoryName);
        $(".lbl_Assignee").text(res[0].Assignee == null ? "" : res[0].Assignee);
        $(".lblPriority").text(res[0].Priority);
        $(".lblRequestType").text(res[0].RequestTypeName);
        $(".lblMethodOfContact").text(res[0].RequestMode);
        $(".Title").html(res[0].Tittle);
        $(".Description").html(res[0].Description);
        $(".lblTypeOfIssue").html(res[0].TypeOfIssue);
        $(".lblSubcategory").html(res[0].Subcategory);
        $(".lblTypeOfComplaint").html(res[0].TypeOfComplaint);
        $(".lblProductSubCategory").html(res[0].ProductSubCategory);

        var val = 0;
        let GenerateDiv = '';
        var DynamicFields = jQuery.parseJSON(Result_[8]);
        var DynamicFieldsValues = jQuery.parseJSON(Result_[9]);
        for (var item in DynamicFields) {
            var DynamicDiv = "";
            if (val == 4 || val == 0) {
                val = 0;
                DynamicDiv += '<div class="controls-row">'
            }

            DynamicDiv += '<div class="control-group span3">';
            DynamicDiv += '<label class="label-bold">' + DynamicFields[item].Caption + '</label>';
            DynamicDiv += '<div class="controls">';

            //if (DynamicFields[item].InputClass == 'ReportedBy') {
            //    var ReportedByName = ReportedBy.find(x => x.Id == DynamicFields[item].RequestTypeSubcategoryFieldTypeDetailId).Value;
            //    DynamicDiv += '<asp:Label runat="server" ID="lblAltNumber" CssClass="span12 lblAltNumber">' + ReportedByName + '</asp:Label>';
            //}
            //else {
            //    DynamicDiv += '<asp:Label runat="server" ID="lblAltNumber" CssClass="span12 lblAltNumber">' + DynamicFields[item].FieldValue + '</asp:Label>';
            //}

            if (DynamicFields[item].FieldTypeId == 2) {   // For DropDown
                var Value = DynamicFieldsValues.find(x => x.Id == DynamicFields[item].RequestTypeSubcategoryFieldTypeDetailId)?.Value;
                Value = Value == undefined ? "" : Value;
                DynamicDiv += '<asp:Label runat="server" ID="lblAltNumber" CssClass="span12 lblAltNumber">' + Value + '</asp:Label>';
            }
            else if (DynamicFields[item].FieldTypeId == 8) {  // For FileUploader

                var path = "/Uploads/" + DynamicFields[item].FieldValue;
                DynamicDiv += '<a href=' + path+ ' target="_blank">View Image</a>';
            }
            else {
                DynamicDiv += '<asp:Label runat="server" ID="lblAltNumber" CssClass="span12 lblAltNumber">' + DynamicFields[item].FieldValue + '</asp:Label>';
            }

            DynamicDiv += '</div>';
            val = val + 1;

            DynamicDiv += '</div>';

            if (val == 4) {
                DynamicDiv += '</div>'
            }
            GenerateDiv += DynamicDiv;
        }
        $('.dynamicFields').html(GenerateDiv);
       
        var ToEmail = jQuery.parseJSON(Result_[1]);
        var CCEmail = jQuery.parseJSON(Result_[2]);
        $('.txtToEmail').val(ToEmail);
        $('.txtCCEmail').val(CCEmail);

        var _DepartmentId = res[0].DepartmentId == null ? "0" : res[0].DepartmentId;
        var DepartmentList = jQuery.parseJSON(Result_[3]);
        FillDropDownByReference(".ddlDepartment", DepartmentList);
        $(".ddlDepartment").val(_DepartmentId);

        var RequestTypeId = res[0].RequestTypeId == null ? "0" : res[0].RequestTypeId;
        var RequestTypeList = jQuery.parseJSON(Result_[5]);
        FillDropDownByReference(".ddlRequestType", RequestTypeList);
        $(".ddlRequestType").val(RequestTypeId);

        var CategoryList = jQuery.parseJSON(Result_[6]);
        var CategoryId = res[0].CategoryId;
        FillDropDownByReference(".ddlCategory", CategoryList);
        $(".ddlCategory").val(CategoryId == null ? "0" : CategoryId);

        var SubcategoryList = jQuery.parseJSON(Result_[7]);
        var SubcategoryId = res[0].SubcategoryId;
        FillDropDownByReference(".ddlSubcategory", SubcategoryList);
        $(".ddlSubcategory").val(SubcategoryId == null ? "0" : SubcategoryId);

        getAssigneeByDepartmentId(_DepartmentId, 0);

        GetTypeOfIssue();

        if (RoleCode === Role.Level_2_3) {
            if (_DepartmentId === parseInt(hf_DepartmentId)) {
                if (parseInt(AssigneeId) !== parseInt(hf_UserId) && AssigneeId !== "0") {
                    $('.HideAll').hide();
                }
            } else {
                $('.HideAll').hide();
            }
            $(".btn_DeleteImage").hide();
        }

        if (StatusId === TicketStatus.Closed || StatusId === TicketStatus.ReOpen) {
            if (RoleCode === Role.Admin) {
                BindDropDownStatusInProgress();
            } else if (StatusId === TicketStatus.ReOpen && RoleCode === Role.Level_1) {
                BindDropDownStatusInProgress();
            }
            else {
                $('.Btn_UploadDiv').hide();
                $('.HideAll').hide();
                $('.DivStatus').hide();
                $('.btn_DeleteImage').hide();
                DisableControls();
            }
        } else if (StatusId === TicketStatus.Cancel) {
            $('.Btn_UploadDiv').hide();
            $('.HideAll').hide();
            $('.DivStatus').hide();
            $('.btn_DeleteImage').hide();
            DisableControls();
        } else if (StatusId === TicketStatus.Junk) {
            $('.HideAll').hide();
            DisableControls();
            BindDropDownStatusCancel();
            $(".ddlStatus").removeAttr('disabled');
            $(".txtDescription").removeAttr('disabled');
            $(".btn_Save").show();
        }
        else {
            var RoleCode = $('.hf_RoleCode').val();
            BindDropDownStatus(RoleCode, null);
        }


    }

    ProgressBarHide();
}


//function getAssignee(DepartmentId, LevelId) {
//    var service = new CyberTicketService.TicketService();
//    ProgressBarShow();
//    service.getAssignee(DepartmentId, LevelId, ongetAssignee, null);
//}
//function ongetAssignee(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlAssignee", res);
//    ProgressBarHide();
//}


function Search() {
    if ($('.txtSearchContact').val() === "" && $('.txtSearchEmail').val() === "") {
        AlertBox("Alert", "Please enter Contact No or Email to search.", "warning");
    } else {
        $('.pnlCustomer').show();
        $('.btnCancelSearchCustomer').show();
        var contactSearch = $('.txtSearchContact').val().trim();
        var emailSearch = $('.txtSearchEmail').val().trim();
        GetSearchCustomer(contactSearch, emailSearch);
    }
}

function GetSearchCustomer(contactSearch, emailSearch) {
    var service = new CyberTicketService.TicketService();
    service.GetSearchCustomer(contactSearch, emailSearch, ongetCustomer, null, null);
}
function ongetCustomer(result) {
    var res = jQuery.parseJSON(result);
    if (res[0].CustomerId != "0") {
        $('.pnlCustomer').show();
        $(".hfCustomerId").val(res[0].CustomerId);
        $(".txtName").val(res[0].CustomerName);
        $(".txtContact").val(res[0].ContactNo);
        $(".txtEmail").val(res[0].EmailAddress);
    } else {
        $('.pnlCustomer').hide();
        $('.txtName').val("");
        $('.txtContact').val("");
        $('.txtEmail').val("");
        AlertBox('Alert', 'No result found.', 'info');
    }

}

function HideDisableButton() {


    $(".DivStatus").hide();
    $(".ddlServiceCategory").attr('disabled', 'disabled');
    $(".ddlAssignee").attr('disabled', 'disabled');
    //$(".btn_UpdateAssignee").hide();
    $(".ddlDepartment").attr('disabled', 'disabled');
    $(".Btn_UploadDiv").hide();
    $(".btn_UpdateToEmail").hide();
    $(".btn_UpdateCCEmail").hide();
    $(".txtCCEmail").attr('disabled', 'disabled');
    $(".txtToEmail").attr('disabled', 'disabled');
    $('.tr_attachment').each(function () {
        $('.btn_DeleteImage').hide();
    });
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
    var service = new CyberTicketService.TicketService();
    service.getSubcategory(CategoryId, ongetSubcategory, null, null);
}
function ongetSubcategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSubcategory", res);
}

function BindDropDownStatus(RoleCode, hf_IsInitiator) {
    var service = new CyberTicketService.TicketService();

    service.GetStatusByRole(RoleCode, hf_IsInitiator, onBindDropDownStatus, null, null);
}

function onBindDropDownStatus(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlStatus", res);
    $(".ddlStatus").val($(".hf_StatusId").val());
}

function BindDropDownStatusInProgress() {
    var service = new CyberTicketService.TicketService();
    service.GetStatusInProgress(onBindDropDownStatusInProgress, null, null);
}

function onBindDropDownStatusInProgress(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlStatus", res);
    $(".ddlStatus").val($(".hf_StatusId").val());
}

function BindDropDownStatusCancel() {
    var service = new CyberTicketService.TicketService();
    service.GetStatusCancel(onBindDropDownStatusCancel, null, null);
}

function onBindDropDownStatusCancel(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlStatus", res);
    $(".ddlStatus").val($(".hf_StatusId").val());
}

function DeleteAtachment(FileId) {
    var service = new CyberTicketService.TicketService();
    service.DeleteAtachment(FileId, onDeleteAtachment, null, null);
}

function onDeleteAtachment(result) {
    var res = jQuery.parseJSON(result);
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        GetSaveAtachment(hf_TicketMasterId, "0", "");
    }
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function FillRFODropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
    $(DropDownReference).append('<option value="-1">Other</option>');
}

function Save() {
    if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0" && $(".ddlDepartment").val() != null) {
        if ($(".ddlStatus").val() != "" && $(".ddlStatus").val() != "0" && $(".ddlStatus").val() != null) {
            var StatusId = $(".ddlStatus").val();
            if (($(".ddlTypeOfIssue").val() != "" && $(".ddlTypeOfIssue").val() != "0" && $(".ddlTypeOfIssue").val() != null) || StatusId == 2) {
                if (($(".ddlProductSubCategory").val() != "" && $(".ddlProductSubCategory").val() != "0" && $(".ddlProductSubCategory").val() != null) || StatusId == 2) {
                    var IsOk = false;
                    var TypeOfComplaintId = $(".ddlTypeOfComplaint").val();
                    if (StatusId === TicketStatus.Closed.toString()) {
                        if ($(".ddlTypeOfComplaint").val() != "" && $(".ddlTypeOfComplaint").val() != "0" && $(".ddlTypeOfComplaint").val() != null) {
                            if (TypeOfComplaintId > 0) {
                                IsOk = true;
                            } else {
                                AlertBox("Alert", "Please select Findings", "warning");
                            }
                        } else {
                            AlertBox("Alert", "Please select Findings", "warning");
                        }
                    }
                    else {
                        TypeOfComplaintId = 0;
                        IsOk = true;
                    }
                    if (IsOk == true) {
                        if ($(".txtDescription").val() != "" && $(".txtDescription").val() != null) {
                            //ConfirmBox("Are You Sure", "Do you want to update?", "warning")
                            var hf_TicketMasterId = $(".hf_TicketMasterId").val();
                            var DepartmentId = $(".ddlDepartment").val();
                            var AssigneeId = $(".ddlAssignee").val();
                            var Description = $(".txtDescription").val();
                            var TypeOfIssueId = $(".ddlTypeOfIssue").val();
                            var ProductSubCategoryId = $(".ddlProductSubCategory").val();
                                                     
                            Save_(hf_TicketMasterId, DepartmentId, AssigneeId, StatusId, TypeOfIssueId, Description, TypeOfComplaintId, ProductSubCategoryId);
                        } else { AlertBox("Alert", "Please enter Description", "warning"); }
                    }
                } else { AlertBox("Alert", "Please select Product Sub-Category", "warning"); }
            } else { AlertBox("Alert", "Please select Product", "warning"); }
        } else { AlertBox("Alert", "Please select Status", "warning"); }
    } else { AlertBox("Alert", "Please select Department", "warning"); }
}

function Save_(TicketMasterId, DepartmentId, AssigneeId, StatusId, TypeOfIssueId, Description, TypeOfComplaintId, ProductSubCategoryId) {
    ProgressBarShow();
    var Ticket_EmailTo = $(".txtToEmail").val().trim();
    var Ticket_EmailCC = $(".txtCCEmail").val().trim();
    var service = new CyberTicketService.TicketService();
    service.UpdateAssignee_(TicketMasterId, DepartmentId, AssigneeId, StatusId, TypeOfIssueId, Description, TypeOfComplaintId, Ticket_EmailTo, Ticket_EmailCC, ProductSubCategoryId, onSave_, null, null);
}

function onSave_(result) {
    if (result == "1") {
        $(".txtDescription").val('');

        $(".ddlStatus").val('0');
        AlertBoxRedirect("Success!", "Updated successfully", "success");
    } ProgressBarHide();
}

function ChangeCustomer(TicketMaster, customer) {
    var service = new CyberTicketService.TicketService();
    service.ChangeCustomer(TicketMaster, customer, onChange_, null, null);
}

function onChange_(result) {
    if (result != "") {
        OnLoad();

        AlertBox("Success!", "Customer has been assigned successfully.", "success");
    } else {
        AlertBox("Error!", "Unable to assign customer.", "warning");
    }
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

function AlertBoxRedirect(title, Message, type) {
    swal({
        title: title,
        text: Message,
        type: type
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

function ConfirmBox(title, Message, type) {
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
                var hf_TicketMasterId = $(".hf_TicketMasterId").val();
                var DepartmentId = $(".ddlDepartment").val();
                var AssigneeId = $(".ddlAssignee").val();
                var StatusId = $(".ddlStatus").val();
                var Description = $(".txtDescription").val();
                Save_(hf_TicketMasterId, DepartmentId, AssigneeId, StatusId, Description);
            } else {
            }
        });

}

function UpdateAssignee() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        var IsValidate = ControlValidateAssignee();
        if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0" && $(".ddlDepartment").val() != null) {
            if ($(".ddlServiceCategory").val() != "" && $(".ddlServiceCategory").val() != "0" && $(".ddlServiceCategory").val() != null) {
                if (IsValidate == true) {
                    var DepartmentId = $(".ddlDepartment").val() == "0" ? null : $(".ddlDepartment").val();
                    var AssigneeId = $(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val();
                    var ServiceCategoryId = $(".ddlServiceCategory").val() == "0" ? null : $(".ddlServiceCategory").val();
                }
                UpdateAssignee_(hf_TicketMasterId, DepartmentId, AssigneeId, ServiceCategoryId);
            }
            else { AlertBox("Alert", "Please select Problem Category", "warning"); }
        }
        else { AlertBox("Alert", "Please select Department", "warning"); }

    }
}

function UpdateAssignee_(TicketMasterId, DepartmentId, AssigneeId, ServiceCategoryId) {
    var service = new CyberTicketService.TicketService();
    service.UpdateAssignee_(TicketMasterId, DepartmentId, AssigneeId, ServiceCategoryId, onUpdateAssignee_, null, null);
}

function onUpdateAssignee_(result) {

    if (result == "1") {
        AlertBox("Success!", "Assigned successfully", "success");
        OnLoad();
    }
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

function UpdateToEmail() {
    $(".txtToEmail").css("border-color", "#d4d4d4");
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        if ($(".txtToEmail").val().trim() != "") {
            var emailtextboxvalue = $('.txtToEmail').val().trim();
            var lastChar = emailtextboxvalue.substr(emailtextboxvalue.length - 1); // => "1"
            if (lastChar != ";") {
                emailtextboxvalue = emailtextboxvalue + ";";
            }
            var result = validateMultipleEmailsCommaSeparated(emailtextboxvalue, ';');
            if (result == true) {
                var array = emailtextboxvalue.split(';');
                var uniques = _.uniq(array);
                uniques = uniques.join(';');
                $('.txtToEmail').val(uniques)
                var service = new CyberTicketService.TicketService();
                service.UpdateTicketToEmails(hf_TicketMasterId, uniques, onUpdateTicketToEmails, null, null);
            } else { $(".txtToEmail").css("border-color", "Red"); AlertBox("Alert", "Invalid To Email Address", "warning"); }
        } else { $(".txtToEmail").css("border-color", "Red"); AlertBox("Alert", "Please enter Email To", "warning"); }
    }
}

function onUpdateTicketToEmails(result) {

    if (result == "1") {
        AlertBox("Success!", "Email To Updated Successfully", "success");
    }
}

function UpdateCCEmail() {
    $(".txtCCEmail").css("border-color", "#d4d4d4");
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        if ($(".txtCCEmail").val().trim() != "") {
            var emailtextboxvalue = $('.txtCCEmail').val().trim();
            var lastChar = emailtextboxvalue.substr(emailtextboxvalue.length - 1); // => "1"
            if (lastChar != ";") {
                emailtextboxvalue = emailtextboxvalue + ";";
            }
            var result = validateMultipleEmailsCommaSeparated(emailtextboxvalue, ';');
            if (result == true) {
                var array = emailtextboxvalue.split(';');
                var uniques = _.uniq(array);
                uniques = uniques.join(';');
                $('.txtCCEmail').val(uniques)
                var service = new CyberTicketService.TicketService();
                service.UpdateTicketCCEmails(hf_TicketMasterId, uniques, onUpdateTicketCCEmails, null, null);
            } else { $(".txtCCEmail").css("border-color", "Red"); AlertBox("Alert", "Invalid CC Email Address", "warning"); }
        } else { $(".txtCCEmail").css("border-color", "Red"); AlertBox("Alert", "Please enter Email CC", "warning"); }
    }
}

function onUpdateTicketCCEmails(result) {
    if (result == "1") {
        AlertBox("Success!", "Email CC Updated Successfully", "success");
    }
}

function validateMultipleEmailsCommaSeparated(emailcntl, seperator) {
    var value = emailcntl;
    if (value != '') {
        var result = value.split(seperator);
        for (var i = 0; i < result.length; i++) {
            if (result[i] != '') {
                if (!validateEmail(result[i])) {
                    return false;
                }
            }
        }
    }
    return true;
}

function validateEmail(field) {
    var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
    return (regex.test(field)) ? true : false;
}

function DisableControls() {
    $(".ddlDepartment").attr('disabled', 'disabled');
    $(".ddlAssignee").attr('disabled', 'disabled');
    $(".ddlStatus").attr('disabled', 'disabled');
    $(".txtDescription").attr('disabled', 'disabled');
    $(".ddlTypeOfIssue").attr('disabled', 'disabled');
    $(".ddlProductSubCategory").attr('disabled', 'disabled');
}
