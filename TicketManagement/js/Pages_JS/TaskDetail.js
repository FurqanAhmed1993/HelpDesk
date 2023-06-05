function OnLoad() {
    GetAssignee();
    var hfTaskMasterId = $(".hfTaskMasterId").val();
    if (hfTaskMasterId != "0" && hfTaskMasterId != "" && hfTaskMasterId != null) {
        getTaskMasterDetails();
        GetSaveAtachment(hfTaskMasterId, "0", "");
    }
}


function GetSaveAtachment(TaskMasterId, TempId, fileDetail) {
    var service = new CyberTicketService.TicketService();

    service.GetSaveTaskAtachment(TaskMasterId, TempId, fileDetail, onGetSaveAtachment, null, null);
}
function onGetSaveAtachment(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfattachment').html('');
    $('#wfattachment').tmpl(res).appendTo(divTbodyGoalFund);
    $(".Attachement").val('');
}

function DeleteAtachment(FileId) {
    var service = new CyberTicketService.TicketService();
    service.DeleteTaskAttachments(FileId, onDeleteAtachment, null, null);
}

function onDeleteAtachment(result) {
    var res = jQuery.parseJSON(result);
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
        GetSaveTaskAtachment(hf_TicketMasterId, "0", "");
    }
}


function UploadImage() {

    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {
        var UploadedFilePath = FileUpload(fileUpload);
        if (UploadedFilePath != "") {
            var hfTaskMasterId = $(".hfTaskMasterId").val();
            if (hfTaskMasterId != "" && hfTaskMasterId != "0") {
                GetSaveAtachment(hfTaskMasterId, "0", UploadedFilePath);
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





function GetAssignee() {
    var service = new CyberTicketService.TicketService();
    service.getInitiator(onGetAssignee, null, null);
}
function onGetAssignee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAssignee", res);

}
function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}


function getTaskMasterDetails() {
    var hfTaskMasterId = $(".hfTaskMasterId").val();
    var service = new CyberTicketService.TicketService();
    service.getTaskMaster(hfTaskMasterId, ongetTaskMasterDetails, null, null);
}



function ongetTaskMasterDetails(result) {
    var res = jQuery.parseJSON(result);
    $(".lblInitiator").text(res[0].Initiator);
    $(".lblDate").text(res[0].Date_);
    $(".lblTime").text(res[0].Time_);
    $(".lblPriority").text(res[0].Priority);
    $(".lblStartTime").text(res[0].StartDate_);
    $(".lblEndTime").text(res[0].EndDate_);
    $(".Title").html(res[0].Tittle);
    $(".Description").val(res[0].Description);
    $(".lblStatus").html(res[0].Status);
    var StatusId = res[0].StatusId;
    BindDropDownStatus(StatusId);
    var Assignee = res[0].Assignee == null ? "" : res[0].Assignee;
    $(".lblAssignee").text(Assignee);
    var IsInitiator = $(".hf_IsInitiator").val();
    if (IsInitiator == 1) {
        $(".DivAssignee").hide();
        if (StatusId == 2 || StatusId == 3 || StatusId == 5) {
            $("#DivStatus").hide();
        }
        else {
            $("#DivStatus").show();
        }

    }
    else {
        var AssineeId = res[0].AssigneeId == null ? "0" : res[0].AssigneeId;
        if (StatusId == 2 || StatusId == 3 || StatusId == 5) {
            $("#DivStatus").hide();
            $(".ddlAssignee").attr('disabled', 'disabled');
            $(".btn_UpdateAssignee").hide();
            $(".Btn_UploadDiv").hide();
            $(".DivAssignee").hide();
        }
        else {
            $("#DivStatus").show();
            $(".ddlAssignee").attr('enabled', 'enabled');
            $(".btn_UpdateAssignee").show();
        }

        if (AssineeId != "0") {
            $(".ddlAssignee").val(AssineeId);
        }
        else {
            $("#DivStatus").hide();
        }
    }


}



function BindDropDownStatus(StatusId) {
    var hf_IsInitiator = $(".hf_IsInitiator").val();
    var service = new CyberTicketService.TicketService();
    service.GetStatus(StatusId, hf_IsInitiator, onBindDropDownStatus, null, null);
}

function onBindDropDownStatus(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlStatus", res);
}

function Save() {

    var hfTaskMasterId = $(".hfTaskMasterId").val();
    if (hfTaskMasterId != "" && hfTaskMasterId != "0") {
        var IsValidate = ControlsValidateFunction();
        if (IsValidate == true) {
            var StatausId = $(".ddlStatus").val() == "0" ? null : $(".ddlStatus").val();
            var Description = $(".txtDescription").val();
            var service = new CyberTicketService.TicketService();
            service.UpdateTaskStatus(hfTaskMasterId, StatausId, Description, onSave_, null, null);
        }
    }
}




function onSave_(result) {

    if (result == "1") {
        $(".ddlStatus").val('0');
        $(".txtDescription").val('');
        getTaskMasterDetails();
        AlertBox("Success!", "Status updated successfully", "success");
    }
}



function AlertBox(title, Message, type) {
    swal(title, Message, type);
}




function UpdateAssignee() {

    var hfTaskMasterId = $(".hfTaskMasterId").val();

    if (hfTaskMasterId != "" && hfTaskMasterId != "0") {
        var IsValidate = ControlValidateAssignee();
        if (IsValidate == true) {
            var AssigneeId = $(".ddlAssignee").val() == "0" ? null : $(".ddlAssignee").val();
            UpdateAssignee_(hfTaskMasterId, AssigneeId);
        }
    }
}

function UpdateAssignee_(TicketMasterId, AssigneeId) {
    var service = new CyberTicketService.TicketService();
    service.UpdateTaskAssignee_(TicketMasterId, AssigneeId, onUpdateAssignee_, null, null);
}

function onUpdateAssignee_(result) {

    if (result == "1") {
        getTaskMasterDetails();
        AlertBox("Success!", "Assigned successfully", "success");
    }
}

