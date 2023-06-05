
var TicketStatus =
    {
        New: 1,
        Cancel: 2,
        Closed: 3,
        Working: 4,
        Resolved: 5,
        OnHold: 6,
        ReOpen: 7,
        Junk: 8
    }

function OnLoad() {

    GetInitiator();
    getPriority();
    GetDepartments();
    $(".ddlDepartment").change(function () {
        getAssignee($(this).val(), 0);
    });


    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var hfTaskMasterId = $(".hfTaskMasterId").val();
    var hf_View = $(".hf_View").val();
    if (hf_TicketMasterId != null || hf_TicketMasterId != "" || hf_TicketMasterId != "0") {
        if (hf_View == "0") {
            $(".Attchemntupload").show();
            $(".AttchemntRepeter").hide();
            $(".Div_ButttonSave").show();
            //ControlEnabled();
            //resetControls();
        }
        else if (hf_View == "1" && hfTaskMasterId != null || hfTaskMasterId != "" || hfTaskMasterId != "0") {

        }
    }
}

function GetDepartments() {
    var service = new CyberTicketService.TicketService();
    service.getDepartments(onGetDepartments, null, null);
}
function onGetDepartments(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDepartment", res);
    $(".ddlDepartment").change();
}

function GetInitiator() {
    var service = new CyberTicketService.TicketService();
    service.getInitiator(onGetInitiator, null, null);
}
function onGetInitiator(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlInitiator", res);
    var UserId = $(".hf_UserId").val();
    $(".ddlInitiator").val(UserId);
}

function getPriority() {

    var service = new CyberTicketService.TicketService();
    service.getPriority(ongetPriority, null, null);
}
function ongetPriority(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlPriority", res);
}


function getAssignee(DepartmentId, LevelId) {
    var service = new CyberTicketService.TicketService();
    service.getAssignee(DepartmentId, LevelId, ongetAssignee, null);
}
function ongetAssignee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAssignee", res);
}


function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function Save() {

    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != null || hf_TicketMasterId != "" || hf_TicketMasterId != "0") {
        var IsValidate = ControlsValidateFunction();
        if ($(".txtTitle").val().trim() != "") {
            if ($(".ddlInitiator").val() != "" && $(".ddlInitiator").val() != "0") {
                if ($(".ddlDepartment").val() != "" && $(".ddlDepartment").val() != "0") {
                    if ($(".txStarttDate").val().trim()) {
                        if ($(".txtEndDate").val().trim()) {
                            if ($(".ddlPriority").val() != "" && $(".ddlPriority").val() != "0") {
                                if ($(".txtDescription").val().trim()) {

                                    if (IsValidate == true) {
                                        var Response_Task = new Object();
                                        Response_Task.TicketMasterId = hf_TicketMasterId;
                                        Response_Task.TaskTitle = $(".txtTitle").val();
                                        Response_Task.InitiatorId = $(".ddlInitiator").val() == "0" ? null : $(".ddlInitiator").val();
                                        Response_Task.AssigneeId = $(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val();
                                        Response_Task.StartDate = $(".txStarttDate").val();
                                        Response_Task.EndDate = $(".txtEndDate").val();
                                        Response_Task.PriorityId = $(".ddlPriority").val() == "0" ? null : $(".ddlPriority").val();
                                        Response_Task.StatusId = TicketStatus.New;
                                        Response_Task.Description = $(".txtDescription").val();
                                        Response_Task.DepartmentId = $(".ddlDepartment").val() == "0" ? null : $(".ddlDepartment").val();
                                        var Response_TaskMaster = JSON.stringify(Response_Task);
                                        var Response_TaskDetail = new Object();
                                        Response_TaskDetail.StatusId = TicketStatus.New;
                                        Response_TaskDetail.AssigneeTo = $(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val();
                                        Response_TaskDetail.Description = $(".txtDescription").val();
                                        var Response_TaskMasterDetail = JSON.stringify(Response_TaskDetail);
                                        var FilePath = "";
                                        var fileUpload = $('.Attachement').get(0);
                                        var filesCount = fileUpload.files.length;
                                        if (filesCount > 0) {
                                            FilePath = FileUpload(fileUpload);
                                        }
                                        var service = new CyberTicketService.TicketService();
                                        service.SaveTask(hf_TicketMasterId, Response_TaskMaster, Response_TaskMasterDetail, FilePath, onSave_, null, null);
                                    }
                                } else { AlertBox("Alert", "Please enter Initial findings", "warning"); }
                            } else { AlertBox("Alert", "Please select Priority", "warning"); }
                        } else { AlertBox("Alert", "Please select End Date", "warning"); }
                    } else { AlertBox("Alert", "Please select Start Date", "warning"); }
                } else { AlertBox("Alert", "Please select Department", "warning"); }
            } else { AlertBox("Alert", "Please select Initiator", "warning"); }
        } else { AlertBox("Alert", "Please enter Title", "warning"); }
    }
}

function onSave_(result) {

    if (result == "1") {

        AlertBox("Success!", "Task has been saved successfully", "success");
        resetControls();
    }
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
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

function resetControls() {
    debugger;
    $(".txtTitle").val("");
    $(".txtLocation").val("");
    $(".txStarttDate").val("");
    $(".txtEndDate").val("");
    $(".Attachement").val('');
    $(".txtDescription").val("");
    var UserId = $(".hf_UserId").val();
    $(".ddlInitiator").val(UserId);
    $(".ddlPriority").val("0");
    $(".ddlDepartment").val("0").change();

}
