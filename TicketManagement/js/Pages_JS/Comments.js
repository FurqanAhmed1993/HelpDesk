
function OnLoad() {
    var hf_TicketMasterId = $(".hf_TicketMasterId").val();
    Get_Comments(hf_TicketMasterId);
}

function Save() {
  
    var IsValidate = ControlsValidateFunction();
    if (IsValidate == true) {
        var Description = $(".txtDescription").val().trim();
        var hf_TicketMasterId = $(".hf_TicketMasterId").val();
        Save_(hf_TicketMasterId, Description);
    }
}

function Save_(TicketMasterId, Description) {

    var service = new CyberTicketService.TicketService();
    service.SaveComments(TicketMasterId, Description, onIsSaveComments, null, null);
}
function onIsSaveComments(result) {

    if (result == "1") {
        var hf_TicketMasterId = $(".hf_TicketMasterId").val();
        $(".txtDescription").val("");
        Get_Comments(hf_TicketMasterId);
    }
}

function Get_Comments(TicketMasterId) {
    var service = new CyberTicketService.TicketService();
    service.Get_Comments(TicketMasterId, onGet_Comments, null, null);
}
function onGet_Comments(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);
}


