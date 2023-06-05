ProgressBarHide();

function ProgressBarShow() {

    $('#waitProgressBar').show();
    $('.loader').show();
}

function ProgressBarHide() {
    $('#waitProgressBar').hide();
    $('.loader').hide();
}

function Dashboard_GetUnreadChatNotification() {

    var service = new CyberTicketService.TicketService();
    service.Dashboard_GetUnreadChat(0, onDashboard_GetUnreadChatNotification, null, null);
}

function onDashboard_GetUnreadChatNotification(result) {

    if (result != "") {
        var result_ = result.split('Split_');
        var res_NotificationCount = jQuery.parseJSON(result_[0]);
        $(res_NotificationCount).each(function (k, v) {
            Notification(v.ChatCount + " unread Messages !!!", "You have " + v.ChatCount + " unread Messages from " + v.Name);
        });
    }
}

function Notification(Title, Msg) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: Title,
        // (string | mandatory) the text inside the notification
        text: Msg,
        //before_close: function (e, manual_close) {
        //    var manually = (manual_close) ? 'The "X" was clicked to close me!' : '';
        //    alert("I am called before it closes: I am passed the jQuery object for the Gritter element... \n" + manually);
        //},
    });
}


function GetURLParameter(sParam) {
    var ReturnVariable = "";
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            ReturnVariable = decodeURIComponent(sParameterName[1]);
        }
    }
    return ReturnVariable;
}