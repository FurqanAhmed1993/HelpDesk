

function OnLoad() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var hf_TicketMeetingId = $(".hf_TicketMeetingId").val();
    var hf_View = $(".hf_View").val();
    if (hf_TicketMasterId != null || hf_TicketMasterId != "" || hf_TicketMasterId != "0") {
        if (hf_View == "0") {
            $(".DivBtnMinutesofMeeting").hide();
            $(".Div_MinutsofMeeting").hide();
            $(".Attchemntupload").show();
            $(".AttchemntRepeter").hide();
            $(".Div_AttendeeView").hide();
            $(".Div_AttendeeSelecttion").show();
            $(".Div_ButttonSave").show();
            ControlEnabled();
            resetControls();
        }
        else if (hf_View == "1" && hf_TicketMeetingId != null || hf_TicketMeetingId != "" || hf_TicketMeetingId != "0") {

            ViewMeeting();
        }
    }
}






function ControlDisable() {

    $(".txtMeetingAgenda").attr('disabled', 'disabled');
    $(".txtLocation").attr('disabled', 'disabled');
    $(".txtDate").attr('disabled', 'disabled');
    $(".txtDescription").attr('disabled', 'disabled');
    $(".ddlHrsFrom").attr('disabled', 'disabled');
    $(".ddlMinFrom").attr('disabled', 'disabled');
    $(".ddlHRsTo").attr('disabled', 'disabled');
    $(".ddlMinTo").attr('disabled', 'disabled');
    $(".txtMinutesOfMeeting").attr('disabled', 'disabled');
}

function ControlEnabled() {
    $(".txtMeetingAgenda").removeAttr('disabled');
    $(".txtLocation").attr('enabled', 'enabled');
    $(".txtDate").attr('enabled', 'enabled');
    $(".txtDescription").attr('enabled', 'enabled');
    $(".ddlHrsFrom").attr('enabled', 'enabled');
    $(".ddlMinFrom").attr('enabled', 'enabled');
    $(".ddlHRsTo").attr('enabled', 'enabled');
    $(".ddlMinTo").attr('enabled', 'enabled');

}

function ViewMeeting() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    var hf_TicketMeetingId = $(".hf_TicketMeetingId").val();
    var service = new CyberTicketService.TicketService();
    service.GetMeeting(hf_TicketMasterId, hf_TicketMeetingId, onViewMeeting_, null, null);

}

function onViewMeeting_(result) {

    $(".DivBtnMinutesofMeeting").show();
    $(".Div_MinutsofMeeting").show();

    $(".Div_AttendeeView").show();
    $(".Div_AttendeeSelecttion").hide();
    $(".Div_ButttonSave").hide();


    var res = jQuery.parseJSON(result);
    var hf_TicketMeetingId = $(".hf_TicketMeetingId").val();
    if (hf_TicketMeetingId != null || hf_TicketMeetingId != "0" || hf_TicketMeetingId != "") {
        var Date = DateFormate(res[0].MeetingDate);
        $(".txtMeetingAgenda").val(res[0].MeetingAgenda);
        $(".txtLocation").val(res[0].Location);
        $(".txtDate").val(Date);
        $(".txtDescription").val(res[0].Description);
        $(".txtMinutesOfMeeting").val(res[0].MeetingDetail);
        var HrsFrom = "";
        var MinFrom = "";
        var HrsTo = "";
        var MinTo = "";
        var StartTime = res[0].MeetingStartTime;
        var EndTime = res[0].MeetingEndTime;
        if (StartTime != "") {
            var startTime_ = StartTime.split(":");
            HrsFrom = startTime_[0];
            MinFrom = startTime_[1];
            $(".ddlHrsFrom").val(HrsFrom);
            $(".ddlMinFrom").val(MinFrom);
        }
        if (EndTime != "") {
            var endTime_ = EndTime.split(':');
            HrsTo = endTime_[0];
            MinTo = endTime_[1];
            $(".ddlHRsTo").val(HrsTo);
            $(".ddlMinTo").val(MinTo);
        }
        ViewMeetingAttendee(hf_TicketMeetingId);

        var HasAttachment = res[0].HasAttachment;
        if (HasAttachment > 0) {
            $(".Attchemntupload").hide();
            $(".AttchemntRepeter").show();
            getMeetingAttachment(hf_TicketMeetingId);
        }
        else if (HasAttachment == 0) {
            $(".divAttachement").hide();
        }
        ControlDisable();





    }
}

function DateFormate(Date_Parameter) {

    var Split = Date_Parameter.split("-");
    var Year = Split[0];
    var Month = Split[1];
    var Split_Day = Split[2].split("T");
    var Day = Split_Day[0];
    var Month_;
    if (Month == "01") {
        Month_ = "Jan";
    }
    else if (Month == "02") {
        Month_ = "Feb";
    }
    else if (Month == "03") {
        Month_ = "Mar";
    }
    else if (Month == "04") {
        Month_ = "Apr";
    }
    else if (Month == "05") {
        Month_ = "May";
    }
    else if (Month == "06") {
        Month_ = "Jun";
    }
    else if (Month == "07") {
        Month_ = "Jul";
    }
    else if (Month == "08") {
        Month_ = "Aug";
    }
    else if (Month == "09") {
        Month_ = "Sep";
    }
    else if (Month == "10") {
        Month_ = "Oct";
    }
    else if (Month == "11") {
        Month_ = "Nov";
    }
    else if (Month == "12") {
        Month_ = "Dec";
    }
    var FinalDale = Month_ + " " + Day + "," + Year;
    return FinalDale;
}

function ViewMeetingAttendee(TicketMeetingId) {
    var service = new CyberTicketService.TicketService();
    service.ViewMeetingAttendee(TicketMeetingId, onViewMeetingAttendee_, null, null);
}

function onViewMeetingAttendee_(result) {
    var res = jQuery.parseJSON(result);
    var html = "<br /> <ul  style='text-align: justify;margin-left: 11px;font-size: 15px'>";
    res.forEach(function (obj) {
        html = html + "<li style='margin-bottom: 8px;'>" + obj.Attendee + "</li>";
    });
    html = html + "</ul>";
    $(html).appendTo('#div_AttendeeList');
}

function getMeetingAttachment(TicketMeetingId) {
    var service = new CyberTicketService.TicketService();
    service.getMeetingAttachment(TicketMeetingId, ongetMeetingAttachment_, null, null);
}

function ongetMeetingAttachment_(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfattachment').html('');
    $('#wfattachment').tmpl(res).appendTo(divTbodyGoalFund);
    $(".Attachement").val('');
}

function DeleteAtachment(FileId) {
}

function Save() {

    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    if (hf_TicketMasterId != null || hf_TicketMasterId != "" || hf_TicketMasterId != "0") {
        var IsValidate = ControlsValidateFunction();
        if ($(".txtMeetingAgenda").val() != "") {
            if ($(".txtLocation").val() != "") {
                if ($(".txtDate").val() != "") {
                    if ($(".ddlHrsFrom").val() != "0" && $(".ddlMinFrom").val() != "0") {
                        if ($(".ddlHRsTo").val() != "0" && $(".ddlMinTo").val() != "0") {
                            if (IsValidate == true) {
                                var StartTime = $(".ddlHrsFrom").val() + ":" + $(".ddlMinFrom").val();
                                var EndTime = $(".ddlHRsTo").val() + ":" + $(".ddlMinTo").val();
                                var Response_TicketMeeting = new Object();
                                Response_TicketMeeting.TicketMasterId = hf_TicketMasterId;
                                Response_TicketMeeting.MeetingDate = $(".txtDate").val();
                                Response_TicketMeeting.MeetingAgenda = $(".txtMeetingAgenda").val();
                                Response_TicketMeeting.Location = $(".txtLocation").val();
                                Response_TicketMeeting.Description = $(".txtDescription").val();
                                var Response_TicketMeetingMaster = JSON.stringify(Response_TicketMeeting);
                                var Details = "";
                                $('.ListBoxDestination option').each(function () {
                                    var Response = new Object();
                                    var AttendeeId = $(this).val();
                                    Details += AttendeeId + ",";
                                })
                                var FilePath = "";
                                var fileUpload = $('.Attachement').get(0);
                                var filesCount = fileUpload.files.length;
                                if (filesCount > 0) {
                                    FilePath = FileUpload(fileUpload);
                                }
                                var service = new CyberTicketService.TicketService();
                                service.SaveMeeting(hf_TicketMasterId, Response_TicketMeetingMaster, Details, FilePath, StartTime, EndTime, onSave_, null, null);
                            }
                        } else { AlertBox("Alert", "Please select End  Time", "warning"); }
                    } else { AlertBox("Alert", "Please select Start Time", "warning"); }
                } else { AlertBox("Alert", "Please select Date", "warning"); }
            } else { AlertBox("Alert", "Please enter Location", "warning"); }
        } else { AlertBox("Alert", "Please enter Meeting Agenda", "warning"); }
    }
}


function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

function onSave_(result) {

    if (result == "1") {
        resetControls();
        AlertBox("Success!", "Meeting has been saved successfully", "success");
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

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

function resetControls() {
    $(".txtMeetingAgenda").val("");
    $(".txtLocation").val("");
    $(".txtDate").val("");
    $(".ddlHrsFrom").val("0");
    $(".ddlMinFrom").val("0");
    $(".ddlHRsTo").val("0");
    $(".ddlMinTo").val("0");
    $(".ddlHrsFrom").val("0");
    $(".txtDescription").val("");
    $(".Attachement").val('');
    $(".ddlDepartmentCC").val("0");
    $('.ListBoxDestination option').each(function (index, option) {
        $(option).remove();
    });
    $('.ListBoxSource option').each(function (index, option) {
        $(option).remove();
    });
}


function SaveMinutesOfMeeting() {
    if (ControlsValidateMinutsOfMinuts() == true) {
        var hf_TicketMeetingId = $(".hf_TicketMeetingId").val();
        if (hf_TicketMeetingId != null || hf_TicketMeetingId != "" || hf_TicketMeetingId != "0") {
            var MinutesOfMeeting = $(".txtAddMinutesOfMeeting").val();
            var service = new CyberTicketService.TicketService();
            service.SaveMinutesOfMeeting(hf_TicketMeetingId, MinutesOfMeeting, onSaveMinutesOfMeeting_, null, null);
        }

    }
}


function onSaveMinutesOfMeeting_(result) {

    if (result != "") {
        $(".txtMinutesOfMeeting").val(result);
        ClosePopup();
    }
}
