//ProgressBarShow();
//ProgressBarHide();

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

    //var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    //if (hf_TicketMasterId != "0" && hf_TicketMasterId != "") {
    //    getTicketMasterDetails(hf_TicketMasterId);
    //}
}

function getTicketMasterDetails(TicketMasterId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.getTicketMasterDetails(TicketMasterId, ongetTicketMasterDetails, null, null);
}

function ongetTicketMasterDetails(result) {
    if (result != "") {
        var Result_ = result.split('Split_');
        var res = jQuery.parseJSON(Result_[0]);
        var hf_TicketMasterId = $(".hf_TicketMasterId").val();
        $(".lblTicketNo").text(res[0].TicketCode);
        $(".lbl_IncidentDate_Time").text(res[0].CreatedDate_);
        $(".lbl_ResolutionDate_Time").text(res[0].ResolutionDate);
        $(".lblOutageDuration").text(res[0].OutageDuration);
        $(".lblProduct").text(res[0].Product);
        $(".lblRegion").text(res[0].Region);
        $(".lblCity").text(res[0].City);
        if (res[0].ProductId == 1) {
            if (res[0].SlotPOnONT == "0") {
                $(".DivSlotPONONT").hide();
            }
            else { $(".lbl_SLOT_PON_ONT").text(res[0].SlotPOnONT); }
        }
        else {
            $(".DivSlotPONONT").hide();
        }
        $(".lblPreview").text(res[0].Client + " - " + res[0].TicketCode);
        $(".lblCustomerType").text(res[0].CustomerType);
        $(".lblCustomer").text(res[0].Client);
        $(".lblCustomerDetail").text(res[0].ClientDetail);
        $(".lbl_IncidentDateTime").text(res[0].CreatedDate_);
        $(".lbl_IncidentDateTimeToCustomer").text(res[0].CreatedDate_);
        $(".lbl_IncidentEscalatedToTACLEVEL_1_Team_For_Support").text(res[0].FirstReplyToCustomer_);
        $(".lbl_IncidentEscalatedToTACLEVEL_2_Team_For_Support").text(res[0].IncidentEscalatedToTACLEVEL_2_Team_For_Support_);
        $(".lbl_IncidentResolutionacknowledgementbyTACLEVEL_2_Team").text(res[0].IncidentResolutionacknowledgementbyTACLEVEL_2_Team);
        $(".lbl_IncidentResolutionacknowledgementbyTACLEVEL_1_Team").text(res[0].ResolutionDate);
        $(".lbl_IncidentresolutionnotificationtocustomerbyHelpdesk").text(res[0].ResolutionDate);
    }

    ProgressBarHide();
}

//function Preview() {
  
//    var HasImage = false;
//    var IsValidate = ControlsValidateFunction();
//    if ($(".txtOutageSynopsis").val().trim() != "") {
//        if ($(".txtCorrective_FixActions").val().trim() != "") {
//            debugger;
//            var list = document.getElementById("ContentPlaceHolder1_rblImages"); //Client ID of the radiolist
//            var inputs = list.getElementsByTagName("input");
//            var selected;
//            for (var i = 0; i < inputs.length; i++) {
//                if (inputs[i].checked) {
//                    selected = inputs[i];
//                    HasImage = true
//                    break;
//                }
//            }
//            var hf_TicketMasterId = $(".hf_TicketMasterId").val();
//            var service = new CyberTicketService.TicketService(); 
//            service.RFO(hf_TicketMasterId, $(".lblCustomer").text(), $(".lblTicketNo").text(), true, $(".txtOutageSynopsis").val().trim(), $(".txtCorrective_FixActions").val().trim(), $(".lbl_IncidentDateTime").text(), $(".lbl_IncidentDateTimeToCustomer").text(), $(".lbl_IncidentEscalatedToTACLEVEL_1_Team_For_Support").text(), $(".lbl_IncidentEscalatedToTACLEVEL_2_Team_For_Support").text(), $(".lbl_IncidentResolutionacknowledgementbyTACLEVEL_2_Team").text(), $(".lbl_IncidentResolutionacknowledgementbyTACLEVEL_1_Team").text(), $(".lbl_IncidentresolutionnotificationtocustomerbyHelpdesk").text(), HasImage, onPreviewReturn_, null, null);
           
//        } else { AlertBox("Alert", "Please enter Corrective / Fix Actions", "warning"); }
//    } else { AlertBox("Alert", "Please enter Outage Synopsis", "warning"); }
//}


function onPreviewReturn_(result) {
    if (result =! "") {
        alert(result);
        //$("hlDownload").attr("href", "http://www.google.com/")
        //$('hlDownload');
       // OpenPopup();
    }
}


function ControlsValidateFunction() {
    var State = true;
    $(".TextBoxValidate").each(function () {
        if ($(this).val() == "") {
            $(this).css("border-color", "Red");
            State = false;
        }
        else {
            $(this).css("border-color", "#d4d4d4");
        }
    });
    return State;
}

function Save() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    //var hf_CloseTicketStatus = $(".hf_CloseTicketStatus").val();
    //var hf_ResolvedTicketStatus = $(".hf_ResolvedTicketStatus").val();
    //if (hf_TicketMasterId != "" && hf_TicketMasterId != "0") {
    //    var IsValidate = ControlsValidateFunction();
    //    if (IsValidate == true) {
    //        var StatausId = $(".ddlStatus").val() == "0" ? null : $(".ddlStatus").val();
    //        var Description = $(".txtDescription").val();
    //        var IsTrue = true;
    //        if (StatausId == hf_CloseTicketStatus || StatausId == hf_ResolvedTicketStatus) {
    //            if (Description == "") {
    //                IsTrue = false;
    //            }
    //        }
    //        if (IsTrue == true) {
    //            Save_(hf_TicketMasterId, StatausId, Description);
    //        } else { AlertBox("Alert", "Please enter findings", "warning"); }
    //    }
    //}
}

function Save_(TicketMasterId, StatausId, Description) {

    var service = new CyberTicketService.TicketService();
    //  service.UpdateTicketStatusByinitiator(TicketMasterId, StatausId, Description, onSave_, null, null);
}

function onSave_(result) {

    if (result == "1") {
        $(".ddlStatus").val('0');
        $(".txtDescription").val('');
        OnLoad();
        AlertBox("Success!", "Status updated successfully", "success");
    }
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}


