
var Role =
{
    SuperAdmin: "18",
    Admin: "19",
    Level_1: "20",
    Level_2_3: "28"
}

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

function OnLoad() {

    let RoleId = $(".hfRoleId").val();
    if (RoleId === Role.Admin || RoleId === Role.SuperAdmin || RoleId === Role.Level_1) {
        $('.btnCreateTicket').show();
    } else if (RoleId === Role.Level_2_3) {
        $('.btnCreateTicket').hide();
    }

    var StatusId = GetURLParameter('StatusId');
    if (StatusId == "") {
        SetDate();
    }

    var FromDate = GetURLParameter('FromDate');
    var ToDate = GetURLParameter('ToDate');
    if (FromDate != "" && ToDate != "") {

        SetParamererDate(FromDate, ToDate)
    }

    FillControls();
    getAssignee(0);

    $(".ddlDepartmentSearch").change(function () {
        getAssignee($(this).val());
    });

}


function SetStatus(StatusIds) {
    var Split = StatusIds.split(',');
    for (var i = 0; i < Split.length; i++) {
        var MainId = Split[i];
        if (MainId != "") {
            $(".dvCheckBoxListControl").find("input[type=checkbox]").each(function () {
                var Id = $(this).val();
                if (MainId == Id) {
                    $(this).attr('checked', true);
                }
            });
        }
    }
}

function BindCheckBoxList(checkboxlistItems) {
    $('#dvCheckBoxListControl').html('').remove;
    var table = $('<table></table>').append($('<tr></tr>'));
    var counter = 0;
    $(checkboxlistItems).each(function () {
        table.append($('<td style="display: inline-flex;margin-left: 20px" ></td>').append($('<input>').attr({
            type: 'checkbox', name: 'chklistitem', value: this.Id, id: 'chklistitem_' + this.Id
        })).append(
            $('<label style="margin-left: 5px">').attr({
                for: 'chklistitem' + counter++
            }).text(this.Value)));
    });
    $('#dvCheckBoxListControl').append(table);
    var StatusId_ = GetURLParameter('StatusId');
    if (StatusId_ > 0) {
        $('.txtTicketDateFrom').val();
        $('.txtTicketDateTo').val();
        StatusId_ = StatusId_ + ","
        SetStatus(StatusId_);
    }
    else {
        StatusId_ = "1,2,3,4,5,6,7,8";
        SetStatus(StatusId_);
    }
    var TSNo = GetURLParameter('TsNo');
    if (TSNo != "") {
        $('.txtTicketDateFrom').val('');
        $('.txtTicketDateTo').val('');
        $('.txtTicket').val(TSNo);
    }
    var IsROT = false;
    var IsICOT = false;
    var ROT = GetURLParameter('ROT');
    var ICOT = GetURLParameter('ICOT');
    if (ROT > 0) {
        $('.txtTicketDateFrom').val('');
        $('.txtTicketDateTo').val('');
        IsROT = true;
    }
    else if (ICOT > 0) {
        $('.txtTicketDateFrom').val('');
        $('.txtTicketDateTo').val('');
        IsICOT = true;
    }

    // SetDate();
    Search(IsROT, IsICOT);
}

function FillControls() {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getInitiator_getDepartments_getPriority(onFillControls, null, null);
}

function onFillControls(result) {
    
    var result_ = result.split('Split_');
    var Initiator = jQuery.parseJSON(result_[0]);
    var Department = jQuery.parseJSON(result_[1]);
    var Priority = jQuery.parseJSON(result_[2]);
    var Status = jQuery.parseJSON(result_[3]);
    var City = jQuery.parseJSON(result_[4]);
    var ReportedBy = jQuery.parseJSON(result_[5]);
    FillDropDownByReference(".ddlInitiatorSearch", Initiator);
    FillDropDownByReference(".ddlDepartmentSearch", Department);
    FillDropDownByReference(".ddlPrioritySearch", Priority);
    FillDropDownByReference(".ddlCity", City);
    FillDropDownByReference(".ddlCity", City);
    FillDropDownByReference(".ddlReportedBy", ReportedBy);

    BindCheckBoxList(Status);
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
}

function GetCustomer(CustomerTypeId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getCustomer(CustomerTypeId, onGetCustomer, null, null);
}

function onGetCustomer(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCustomerSearch", res);

    var CustomerId = $('.hfCustomerId').val();
    if (CustomerId > 0) {
        $(".ddlCustomerSearch").val(CustomerId);
        //$(".ddlCustomerSearch").attr('disabled', 'disabled');
    }
    $(".ddlCustomerSearch").change();
    ProgressBarHide();
}

function GetCustomerAddress(CustomerId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.GetCustomerAddress(CustomerId, onGetCustomerAddress, null, null);

}

function onGetCustomerAddress(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAddressSearch", res);
    ProgressBarHide();
}

function getAssignee(DepartmentId) {
    var service = new CyberTicketService.TicketService();
    ProgressBarShow();
    service.getAssigneeByDepartmentId(DepartmentId, ongetAssignee, null);
}

function ongetAssignee(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlAssigneeSearch", res);
    ProgressBarHide();
}

function Search(IsResponseOverTickets, IsInternalChatOverTickets) {
    var DateFrom = $('.txtTicketDateFrom').val();
    var DateTo = $('.txtTicketDateTo').val();

    var dFrom = new Date(DateFrom);
    var dTo = new Date(DateTo);

    if (dFrom > dTo) {
        AlertBox('Date is Incorrect', 'Ticket Created Date To should be greater than Ticket Created Date From.', 'warning');
        return false;
    } else {

        var InitiatorId = $(".ddlInitiatorSearch").val() == "" ? null : ($(".ddlInitiatorSearch").val() == "0" ? null : $(".ddlInitiatorSearch").val());
        var DepartmentId = $(".ddlDepartmentSearch").val() == "" ? null : ($(".ddlDepartmentSearch").val() == "0" ? null : $(".ddlDepartmentSearch").val());
        var AssigneeId = $(".ddlAssigneeSearch").val() == "" ? null : ($(".ddlAssigneeSearch").val() == "0" ? null : $(".ddlAssigneeSearch").val());
        var PriorityId = $(".ddlPrioritySearch").val() == "" ? null : ($(".ddlPrioritySearch").val() == "0" ? null : $(".ddlPrioritySearch").val());
        var CityId = $(".ddlCity").val() == "" ? null : ($(".ddlCity").val() == "0" ? null : $(".ddlCity").val());
        var StatusId = "";

        $(".dvCheckBoxListControl").find("input[type=checkbox]").each(function () {
            if ($(this).is(":checked")) {

                if ($(this).val() != "0") {
                    StatusId += $(this).val() + ",";
                }
            }
        });
        var Ticket = $('.txtTicket').val().trim() == "" ? null : $('.txtTicket').val().trim();
        var Title = $('.txtTitleSearch').val().trim() == "" ? null : $('.txtTitleSearch').val().trim();
        var CustomerName = $('.txtCustomerName').val().trim() == "" ? null : $('.txtCustomerName').val().trim();
        var ContactNo = $('.txtContactNo').val().trim() == "" ? null : $('.txtContactNo').val().trim();
        var AltContactNo = $('.txtAltContactNo').val().trim() == "" ? null : $('.txtAltContactNo').val().trim();
        var EmailAddress = $('.txtEmail').val().trim() == "" ? null : $('.txtEmail').val().trim();
        var ReportedBy = $(".ddlReportedBy").val() == "" ? null : ($(".ddlReportedBy").val() == "0" ? null : $(".ddlReportedBy").val());
        var TicketDateFrom = "";
        var TicketDateTo = "";
        var Operation = "Ticket";
        TicketDateFrom = $('.txtTicketDateFrom').val().trim();
        TicketDateTo = $('.txtTicketDateTo').val().trim();
         
        var service = new CyberTicketService.TicketService();
        ProgressBarShow();
        service.SearchTickets(InitiatorId, DepartmentId, AssigneeId, PriorityId, CityId, Ticket, Title, StatusId, IsResponseOverTickets, IsInternalChatOverTickets, TicketDateFrom, TicketDateTo, CustomerName, ContactNo, AltContactNo, EmailAddress, ReportedBy, Operation, ongetSearch, null, null);
    }
}

function ongetSearch(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);

    $('.tr').each(function () {

        $(this).find('.cls_reopen').hide();
        var StatusId = $(this).find(".hfTicketStatusId").val();
        if (StatusId == Status.ReOpen) {
            $(this).find('.Td_Status').addClass("blink_red");
        }
        var Reopen_Date = $(this).find('.hfReopen_Date').val();
        if (Reopen_Date != null && Reopen_Date != undefined && Reopen_Date != "") {
            $(this).find('.cls_reopen').show();
        }


        if ($(this).closest("tr").find(".hfReplyOtherUser").val() == '1' && StatusId != 3 && StatusId != 2)
            $(this).find('.lnkbtnTicketDetail').addClass("blink_red");


        $(this).find('.btn_Delete').hide();
        var IsAdmin = $(".HfIsAdmin").val();
        var IsSuperAdmin = $(".HfIsSuperAdmin").val();
        var CustomerId = $(".hfCustomerId").val();
        var DepartmentId = $(".hfDepartmentId").val();
        var RoleId = $(".hfRoleId").val();
        var UserId = $(".hfUserId").val();
        var AssigneeId = $(this).find(".hfTicketAssigneeId").val();

        var LinkCount = $(this).find(".hfLinkCount").val();
        var ChatNotificationCount = $(this).find(".hfChatNotificationCount").val();
        var ClientVisiblity = $(this).find(".hfClientVisiblity").val();
        var InternalChatNotificationCount = $(this).find(".hfInternalChatNotificationCount").val();
        var TicketDepartmentId = $(this).find(".hfTicketDepartmentId").val();


        // && TicketDepartmentId == '4' && StatusId == 4
        if ($(this).closest("tr").find(".hfReplyFromL2").val() == '1' && TicketDepartmentId == 4 && StatusId == 4)
            $(this).find('.lnkbtnTicketDetail').addClass("blink_red");


        var TicketInitiatorId = $(this).find(".hfTicketInitiatorId").val();
        if (StatusId == 8) {
            $(this).find('.tdReply_Responce').hide();
        }
        if (IsAdmin == '1' || IsSuperAdmin == '1') {
            $(this).find('.btn_Delete').show();
        }

        $(this).find('.lblNotification').hide();
        $(this).find('.lblInterchatCount').hide();

        if (ClientVisiblity != "visible") {
            $(this).find('.btnAddComment').hide();
            $(this).find('.DivInterchatCount').hide();
        }
        else {
            if (ChatNotificationCount != "" && ChatNotificationCount != "0") {
                $(this).find('.lblNotification').show();
            }
            if (InternalChatNotificationCount != "" && InternalChatNotificationCount != "0") {
                $(this).find('.lblInterchatCount').show();
            }
        }

        if (LinkCount != "" && LinkCount != "0") {
            $(this).find('.btn_History').show();
        }
        else {
            $(this).find('.btn_History').hide();
        }

        if (RoleId === Role.Admin || RoleId === Role.SuperAdmin || RoleId === Role.Level_1) {

            if (StatusId === Status.Junk || StatusId === Status.New) {
                $(this).find('.btn_Edit').show();
            } else {
                $(this).find('.btn_Edit').hide();
            }
            $(this).find('.btnAddComment').show();
            $(this).find('.btnResponse').show();

        } else if (RoleId === Role.Level_2_3) {
            $(this).find('.btn_Edit').hide();
            //$(this).find('.btnResponse').hide();
            $(this).find('.tdReply_Responce').hide();
        }

        if (RoleId === Role.Admin && StatusId != Status.Closed && StatusId != Status.Junk) {    // show edit for admin only for all status of ticket
            $(this).find('.btn_Edit').show();
        }
    });

    $(".lnkbtnTicketDetail").click(function () {
        var url = "/pages/InitiateTicketsDetails.aspx?ControlFalse=False&&hf_IsInitiator=True&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + " ";
        openPopupFancy(url);
        //$(location).attr('href', url);
        return false;
    });

    $(".btn_Delete").click(function () {
        var TicketMasterId = $(this).closest("tr").find(".hfTicketMasterIdRpt").val();
        if (TicketMasterId != "" && TicketMasterId != "0") {
            ConfirmBox_Input("Are You Sure !!!", "You Want To Delete ???", TicketMasterId);
            //ConfirmBox("Are You Sure !!!", "You Want To Delete ???", "warning", TicketMasterId)
        }
        return false;
    });

    $(".btnRFO").click(function () {
        var url = "/pages/CreateRFO.aspx?TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
        $(location).attr('href', url);
        return false;
    });

    $(".btn_History").click(function () {
        GetLinkHistory($(this).closest("tr").find(".hfProductId").val(), $(this).closest("tr").find(".hfManageSevicesMasterId").val(), $(this).closest("tr").find(".hfTicketMasterIdRpt").val());
        return false;
    });

    $(".btnResponse").click(function () {
        var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.Ticket_Reply_Response').val() + "&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfTicketInitiatorId").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfTicketAssigneeId").val() + "";
        openPopupFancy(Url);
        //$(location).attr('href', Url);
        return false;
    });

    $(".btnAddMeeting").click(function () {
        var Url = "/pages/Meeting.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTicketMeetingId=0";
        openPopupFancy(Url);
        return false;
    });

    $(".btnAddTask").click(function () {
        var Url = "/pages/Task.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTaskMasterId=0";
        openPopupFancy(Url);
        return false;
    });

    $(".btnAddComment").click(function () {
        var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.Ticket_Internel_Chat').val() + "&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfTicketInitiatorId").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfTicketAssigneeId").val() + "";
        openPopupFancy(Url);
        return false;
    });

    $(".btn_Edit").click(function () {
        let LastValue = "";
        let encryptedText = SubmitsEncry("TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + LastValue);
        var url = "/pages/CreateTicket.aspx?" + encryptedText + LastValue;
        //openPopupFancy(Url);
        $(location).attr('href', url);
        //window.location(url);
        return false;
    });

    $(".Td_Status").click(function () {
        GetStatusHistory($(this).closest("tr").find(".hfTicketMasterIdRpt").val());
    });

    //Sleep();
    ProgressBarHide();
    paginateTable('.RptTable', 50);
}

function Sleep() {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.Sleep(ongetSleep, null, null);
}

function ongetSleep(result) {
    ProgressBarHide();
}

function openPopupFancy(URL) {

    $.fancybox.open({
        afterClose: function () {
            $('.btn_Search').trigger('click');
        },
        padding: 5,
        width: 1200,
        height: 1000,
        autoSize: false,
        openEffect: 'elastic',
        openSpeed: 50,
        closeEffect: 'elastic',
        closeSpeed: 50,
        closeClick: true,
        href: URL,
        onClosed: function () {
            window.location.reload(true);
        },
        type: 'iframe',

        overlay: {
            css: {
                'background-color': 'gray'
            }
        }

    });
}

function GetLinkHistory(HfProductId, HfManageSevicesMasterId, TicketId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetLinkHistory(HfProductId, HfManageSevicesMasterId, TicketId, onGetLinkHistory, null, null);
}

function onGetLinkHistory(result) {
    if (result != "") {
        var res = jQuery.parseJSON(result);
        var divTbodyGoalFund = $('.wfLinkHistory').html('');
        $('#wfLinkHistory').tmpl(res).appendTo(divTbodyGoalFund);
        ShowPopupDiv_History();
    }
    ProgressBarHide();
}

function Cancel() {

    SetDate();
    $(".ddlDepartmentSearch").val('0').change();
    $(".ddlPrioritySearch").val('0');
    $(".ddlCity").val('0');
    $('.txtTicket').val('');
    $('.txtTitleSearch').val('');
    $('.txtCustomerName').val('');
    $('.txtContactNo').val('');
    $('.txtAltContactNo').val('');
    $('.txtEmail').val('');
    $('input[type=checkbox]').prop('checked', true);

    var CustomerId = $('.hfCustomerId').val();
    if (CustomerId > 0) {
        var UserId = $('.hfUserId').val();

        $(".ddlInitiatorSearch").val(UserId);

        var CustomerTypeId = $('.hfCustomerTypeId').val();
        $(".ddlCustomerTypeSearch").val(CustomerTypeId).change();

    } else {
        $(".ddlInitiatorSearch").val('0');
        $(".ddlCustomerTypeSearch").val('0').change();
    }


    Search(false, false);
}

function GetStatusHistory(TicketId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetStatusHistory(TicketId, onGetStatusHistory, null, null);
}

function onGetStatusHistory(result) {
    if (result != "") {

        var res = jQuery.parseJSON(result);
        var divTbodyGoalFund = $('.wfTicketHistory').html('');
        $('#wfTicketHistory').tmpl(res).appendTo(divTbodyGoalFund);
        ShowPopupDiv();
    }
    ProgressBarHide();
}

function ShowPopupDiv() {
    $('.DivHideTicketStatusHistory').not(':last').remove();
    $('.DivShowTicketStatusHistory').dialog({
        modal: true,
        width: '60%',
        resizable: false,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });
}

function ConfirmBox(title, Message, type, TicketMasterId) {
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
                var service = new CyberTicketService.TicketService();
                ProgressBarShow();
                service.DeleteTicket(TicketMasterId, onDelete, null, null);
            }
        });
}

function ConfirmBox_Input(title, Message, TicketMasterId) {
    swal({
        title: title,
        text: Message,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        closeOnCancel: true,
        animation: "slide-from-top",
        inputPlaceholder: "Please enter remarks.",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
    },
        function (inputValue) {

            if (inputValue === false) return false;
            if (inputValue.trim() === "") {
                swal.showInputError("You need to enter remarks!");
                return false
            }
            var service = new CyberTicketService.TicketService();
            ProgressBarShow();
            service.DeleteTicket(TicketMasterId, inputValue, onDelete, null, null);
        });
}



function onDelete(result) {
    if (result != "") {
        ProgressBarHide();
        AlertBox("Success!", "Ticket # : <strong>" + result + "</strong> <br /> has been deleted successfully", "success");
        Search(false, false);
    } else { ProgressBarHide(); }
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

function ShowPopupDiv_History() {
    $('.DivHideTicketHistory').not(':last').remove();
    $('.DivShowTicketHistory').dialog({
        modal: true,
        width: '60%',
        resizable: false,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });
}

function paginateTable(cls, lengthPerPage) {

    var totalRows = $(cls).find('tbody tr:has(td)').length;
    var recordPerPage = lengthPerPage;
    var totalPages = Math.ceil(totalRows / recordPerPage);
    var $pages = $(cls).find('tfoot');
    $pages.html('');
    $(".TotalRecords").text(totalRows);
    if (totalRows > 0) {
        var $select = $('<tr><td colspan="1"><select  class="form-control" id="tableSelect"><select> <label style="font-size:12px;font-weight:bold" class="control-label">Total Records : ' + totalRows + '</label></td><td colspan="15"><input type="image" id="prevv" src="/Images/back-32.png" class="btn btn-PrevPaging" title="Previous"  /><i class="fa fa-chevron-left"></i> <input type="image" id="nextt" src="/Images/forward-32.png" class="btn btn-NextPaging" title="Next" /> <i class="fa fa-chevron-right"></i>  </td>  </tr>').appendTo($pages);
        for (i = 0; i < totalPages; i++) {
            $('<option value = "' + (i + 1) + '">' + 'Page: ' + (i + 1) + '</option>').appendTo('#tableSelect');
        }

        $(cls).find('tbody tr:has(td)').hide();
        var tr = $('table tbody tr:has(td)');

        for (var i = 0; i <= recordPerPage - 1; i++) {
            $(tr[i]).show();
        }

        $('#prevv').click(function () {
            if (parseInt($('#tableSelect').val()) != 1)
                $('#tableSelect').val($('#tableSelect').val() - 1).change();
            return false;
        });

        $('#nextt').click(function () {
            if (!(parseInt($('#tableSelect').val()) + 1 > totalPages))
                $('#tableSelect').val(parseInt($('#tableSelect').val()) + 1).change();
            return false;
        });

        $('#tableSelect').change(function (event) {
            $(cls).find('tbody tr:has(td)').hide();
            var nBegin = ($(this).val() - 1) * recordPerPage;
            var nEnd = $(this).val() * recordPerPage - 1;
            $('#recordDisp').text(nBegin + ' ' + nEnd);
            for (var i = nBegin; i <= nEnd; i++) {
                $(tr[i]).show();
            }
        });
    }
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


function SetParamererDate(FromDate, ToDate) {

    var Date1 = new Date(FromDate);
    var dd = Date1.getDate();
    var mm = Date1.getMonth() + 1; //January is 0!
    var yyyy = Date1.getFullYear();
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


    var Date2 = new Date(ToDate);
    var dd1 = Date2.getDate();
    var mm1 = Date2.getMonth() + 1; //January is 0!
    var yyyy1 = Date2.getFullYear();
    if (dd1 < 10) {
        dd1 = '0' + dd1;
    }
    if (mm1 == 1) {
        mm1 = "Jan";
    } else if (mm1 == 2) {
        mm1 = "Feb";
    } else if (mm1 == 3) {
        mm1 = "Mar";
    } else if (mm1 == 4) {
        mm1 = "Apr";
    } else if (mm1 == 5) {
        mm1 = "May";
    } else if (mm1 == 6) {
        mm1 = "Jun";
    } else if (mm1 == 7) {
        mm1 = "Jul";
    } else if (mm1 == 8) {
        mm1 = "Aug";
    } else if (mm1 == 9) {
        mm1 = "Sep";
    } else if (mm1 == 10) {
        mm1 = "Oct";
    } else if (mm1 == 11) {
        mm1 = "Nov";
    } else if (mm1 == 12) {
        mm1 = "Dec";
    }
    var Date1_ = mm1 + " " + dd1 + "," + yyyy1;

    $('.txtTicketDateTo').val(Date1_);



}


