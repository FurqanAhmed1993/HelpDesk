var Status =
{
    New: "1",
    Closed: "3",
    InProgress: "4",
    Resolved: "5",
    OnHold: "6",
    ReOpen: 7,
    Junk: "8"
}
function GetWallBoardData() {
    $(".TotalRecords").text(0);
    var TicketDateFrom = $('.txtTicketDateFrom').val().trim();
    var TicketDateTo = $('.txtTicketDateTo').val().trim();
    var dFrom = new Date(TicketDateFrom);
    var dTo = new Date(TicketDateTo);
    /*var Operation = "Wallboard";*/
    if (dFrom > dTo) {
        AlertBox('Date is Incorrect', 'Ticket Created Date To should be greater than Ticket Created Date From.', 'warning');
        return false;
    } else {
        ProgressBarShow();
        var service = new CyberTicketService.TicketService();
        /* service.SearchTickets(null, null, null, null, null, null, null, false, false, TicketDateFrom, TicketDateTo, null, null, null, Operation, onGetWallBoardData, null, null);*/
        service.GetWallBoardData(0, TicketDateFrom, TicketDateTo, onGetWallBoardData, null, null);
    }
}

function onGetWallBoardData(result) {
    if (result != "") {
        //var result_ = result.split('Split_');
        //var res_StatusCount = jQuery.parseJSON(result_[0]);
        /*var res_LastTenTickets = jQuery.parseJSON(result_[1]);*/

        var result_ = jQuery.parseJSON(result);
        $('.lblDate').text(result_.Table[0].LastDate);
        $('.lblCancel').text(result_.Table[0].Cancel);
        $('.lblClosed').text(result_.Table[0].Closed);
        $('.lblInProgress').text(result_.Table[0].Escalated);
        $('.lblJunk').text(result_.Table[0].New);
        $('.lblNew').text(result_.Table[0].InProgress);
        $('.lblOnHold').text(result_.Table[0].OnHold);
        $('.lblReopen').text(result_.Table[0].ReOpen);
        //$('.lblResolved').text(result_.Table[0].Resolved);
        var divTbodyGoalFund = $('.wfForm').html('');
        $('#wfForm').tmpl(result_.Table1).appendTo(divTbodyGoalFund);
        if (result_.Table1 != null && result_.Table1 != undefined && result_.Table1.length > 0) {

            $(".TotalRecords").text(result_.Table1.length);
            $('.tr').each(function () {
                $(this).find('.cls_reopen').hide();
                var TicketStatusId_ = $(this).find('.hfTicketStatusId').val();
                if (TicketStatusId_ == Status.ReOpen)
                {
                    $(this).find('.Td_Status').addClass("blink_red");
                }
                var Reopen_Date = $(this).find('.hfReopen_Date').val();
                if (Reopen_Date != null && Reopen_Date != undefined && Reopen_Date != "") {
                    $(this).find('.cls_reopen').show();
                }
            });
        }




        $(".Td_Status").click(function () {
            var TicketCode = $(this).closest("tr").find(".hfTicketCode").val();
            if (TicketCode != "") {
                var href = $(this).attr('data-href');
                var Url = "<a href='/Pages/Tickets.aspx?TsNo=" + TicketCode + "'/>";
                var link = $(Url);
                link.attr('target', '_blank');
                window.open(link.attr('href'));
            }
        });
    }
    else {
        var divTbodyGoalFund = $('.wfForm').html('');
        $('.lblDate').text('');
        $('.lblCancel').text('0');
        $('.lblClosed').text('0');
        $('.lblInProgress').text('0');
        $('.lblJunk').text('0');
        $('.lblNew').text('0');
        $('.lblOnHold').text('0');
        $('.lblReopen').text('0');
        //$('.lblResolved').text('0');
    }

    ProgressBarHide();
}

function Cancel() {
    SetDate();
    GetWallBoardData();
}

function SetDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm == 1) {
        mm = "Jan";
    } else if (mm == 2) {
        mm = "Feb";
    } else if (mm == 3) {
        mm = "Mar";
    } else if (mm == 4) {
        mm = "Apr";
    } else if (mm == 5) {
        mm = "May";
    } else if (mm == 6) {
        mm = "Jun";
    } else if (mm == 7) { 
        mm = "Jul";
    } else if (mm == 8) {
        mm = "Aug";
    } else if (mm == 9) {
        mm = "Sep";
    } else if (mm == 10) {
        mm = "Oct";
    } else if (mm == 11) {
        mm = "Nov";
    } else if (mm == 12) {
        mm = "Dec";
    }
    var Date_ = mm + " " + dd + "," + yyyy;

    $('.txtTicketDateFrom').val(Date_);
    $('.txtTicketDateTo').val(Date_);

}