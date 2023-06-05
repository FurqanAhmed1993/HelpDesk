


function OnLoad() {
    var CustomerId = $(".hfCustomerId").val();

    GetCDMSLoad(CustomerId);
    $(".ddlProduct").change(function () {
        GetExchangePOP($(this).val(), $(".ddlCity").val());
    });
    $(".ddlRegion").change(function () {
        GetCity($(this).val());
    });
    $(".ddlCity").change(function () {
        GetExchangePOP($(".ddlProduct").val(), $(this).val());
    });

    if (CustomerId != "0") {
        $(".btnCreateTicket").hide();

        $(".txtCustomer").attr('disabled', 'disabled');
        Search();
    }
}


function GetCDMSLoad(CustomerId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetCDMSLoad(CustomerId, onGetCDMSLoad, null, null);
}

function onGetCDMSLoad(result) {
    var result_ = result.split('Split_');
    var CircuitType = jQuery.parseJSON(result_[0]);
    var Product_Status = jQuery.parseJSON(result_[1]);
    var AllProduct = jQuery.parseJSON(result_[2]);
    var Region = jQuery.parseJSON(result_[3]);
    FillDropDownByReference(".ddlCircuitType", CircuitType);
    FillDropDownByReference(".ddlStatus", Product_Status);
    FillDropDownByReference(".ddlProduct", AllProduct);
    FillDropDownByReference(".ddlRegion", Region);
    FillDropDownByReference(".ddlCity", null);
    FillDropDownByReference(".ddlExchangePOP", null);
    ProgressBarHide();
}

function Search() {
    ProgressBarShow();

    var ProductId = $(".ddlProduct").val() == "" ? null : ($(".ddlProduct").val() == "0" ? null : $(".ddlProduct").val());
    var RegionId = $(".ddlRegion").val() == "" ? null : ($(".ddlRegion").val() == "0" ? null : $(".ddlRegion").val());
    var CityId = $(".ddlCity").val() == "" ? null : ($(".ddlCity").val() == "0" ? null : $(".ddlCity").val());
    var ExchangePOPId = $(".ddlExchangePOP").val() == "" ? null : ($(".ddlExchangePOP").val() == "0" ? null : $(".ddlExchangePOP").val());
    var StatusId = $(".ddlStatus").val() == "" ? null : ($(".ddlStatus").val() == "0" ? null : $(".ddlStatus").val());
    var CircuitTypeId = $(".ddlCircuitType").val() == "" ? null : ($(".ddlCircuitType").val() == "0" ? null : $(".ddlCircuitType").val());
    var Customer = $(".txtCustomer").val().trim() == "" ? null : $(".txtCustomer").val().trim();
    var Address = $(".txtAddress").val().trim() == "" ? null : $(".txtAddress").val().trim();
    var CAM = $(".txtCAM").val().trim() == "" ? null : $(".txtCAM").val().trim();
    var IPAddress = $(".txtIPAddress").val().trim() == "" ? null : $(".txtIPAddress").val().trim();
    var BranchCode = $(".txtBranchCode").val().trim() == "" ? null : $(".txtBranchCode").val().trim();
    var POC = $(".txtPOC").val().trim() == "" ? null : $(".txtPOC").val().trim();
    var POCContact = $(".txtPOCContact").val().trim() == "" ? null : $(".txtPOCContact").val().trim();
    var POCEmail = $(".txtPOCEmail").val().trim() == "" ? null : $(".txtPOCEmail").val().trim();
    var service = new CyberTicketService.TicketService();
    service.GetGlobelSerach(ProductId, RegionId, CityId, ExchangePOPId, Customer, POC, POCEmail, POCContact, CAM, IPAddress, BranchCode, Address, StatusId, CircuitTypeId, ongetSearch, null, null);

}

function ongetSearch(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);

    $('.tr').each(function () {

        var RunningTicketCountAgainstLink = $(this).find(".HfRunningTicketCountAgainstLink").val();
        var LinkCount = $(this).find(".HfLinkCount").val();

        if (RunningTicketCountAgainstLink > 0) {
            $(this).find('.buttonTicket').hide();
        }
        if (LinkCount == 0) {
            $(this).find('.btn_History').hide();
        }
    });

    $(".btn_History").click(function () {
        GetLinkHistory($(this).closest("tr").find(".HfProductId").val(), $(this).closest("tr").find(".HfManageSevicesMasterId").val());
        return false;
    });

    paginateTable('.RptTable', 50);
    ProgressBarHide();
}

function GetLinkHistory(HfProductId, HfManageSevicesMasterId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetLinkHistory(HfProductId, HfManageSevicesMasterId, 0, onGetLinkHistory, null, null);
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


function Cancel() {

    $(".txtCustomer").val("");
    $(".txtAddress").val("");
    $(".txtCAM").val("");
    $(".txtIPAddress").val("");
    $(".txtBranchCode").val("");
    $(".txtPOC").val("");
    $(".txtPOCContact").val("");
    $(".txtPOCEmail").val("");
    $(".ddlStatus").val("0");
    $(".ddlCircuitType").val("0");
    $(".ddlProduct").val("0");
    $(".ddlRegion").val("0").change();
    var CustomerId = $(".hfCustomerId").val();
    if (CustomerId != "0") {
        Search();
    }
    else { var divTbodyGoalFund = $('.wfForm').html(''); }
}

function getAllProduct() {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    var CustomerId = $(".hfCustomerId").val();
    service.getAllProduct(CustomerId, ongetAllProduct, null, null);
}
function ongetAllProduct(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlProduct", res);
    $(".ddlProduct").change();
    ProgressBarHide();
}

function GetCircuitType() {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetCircuitType(onGetCircuitType, null, null);
}
function onGetCircuitType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCircuitType", res);
    ProgressBarHide();
}

function GetProduct_Status() {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetProduct_Status(onGetProduct_Status, null, null);

}
function onGetProduct_Status(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlStatus", res);
    ProgressBarHide();
}

function GetExchangePOP(ProductId, CityId) {
    ProgressBarShow();
    var service = new CyberTicketService.TicketService();
    service.GetExchangePOP(ProductId, CityId, onGetExchangePOP, null, null);
}
function onGetExchangePOP(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlExchangePOP", res);
    ProgressBarHide();
}

function getRegion() {
    var service = new CyberTicketService.TicketService();
    service.getRegion(ongetRegion, null, null);
}
function ongetRegion(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlRegion", res);
    $(".ddlRegion").change();
}

function GetCity(RegionId) {
    var service = new CyberTicketService.TicketService();
    service.GetCity(RegionId, onGetCity, null, null);
}
function onGetCity(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCity", res);
    $(".ddlCity").change();
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
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
