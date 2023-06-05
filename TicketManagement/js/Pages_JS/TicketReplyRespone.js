var chkarray = [];

var ChatTypeId =
{
    Ticket_Reply_Response: 1,
    Task_Reply_Response: 2,
    Ticket_Internel_Chat: 3,
}

function OnLoad() {

    $('.Btn_UploadDiv').hide();
    $('.wfForm').html('');
    chkarray = [];
    $(".hf_FilePath").val('');
    if ($('.hf_ChatTypeId').val() != ChatTypeId.Ticket_Reply_Response) {
        $('.Attachement_div').hide();
    }
    Get_Reply_Response();
}


function Get_Reply_Response() {
    var service = new CyberTicketService.TicketService();
    var hf_MasterId = $(".hf_MasterId").val();
    var hf_ChatTypeId = $(".hf_ChatTypeId").val();
    ProgressBarShow();
    service.Get_Reply_Response(hf_MasterId, hf_ChatTypeId, onGet_Reply_Response, null, null);
}
function onGet_Reply_Response(result) {
    if (result != "") {
        var res = jQuery.parseJSON(result);
        Repeter(res);
    }
    ProgressBarHide();
}
function Repeter(res) {
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);
}


function Save() {

    var IsValidate = ControlsValidateFunction();
    if (IsValidate == true) {
        var hf_ChatTypeId = $(".hf_ChatTypeId").val();
        var Description = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content().trim();
        var hf_MasterId = $(".hf_MasterId").val();
        //var FilePath = "";
        //var fileUpload = $('.Attachement').get(0);
        //var filesCount = fileUpload.files.length;
        //if (filesCount > 0) {
        //  
        //}
        var FilePath = $(".hf_FilePath").val();
        var service = new CyberTicketService.TicketService();
        ProgressBarShow();
        service.IsSaveReply_Response(hf_MasterId, Description, hf_ChatTypeId, FilePath, onIsSaveReply_Response, null, null);
    }
}


function onIsSaveReply_Response(result) {
    if (result != "") {
        $(".hf_FilePath").val('');
        chkarray = [];
        $('#divTab').text('');
        var res = jQuery.parseJSON(result);
        document.getElementById('ContentPlaceHolder1_txtDescription_ctl02_ctl00').contentWindow.document.body.innerHTML = "";
        $('.Attachement').val('');
        Repeter(res);
        ProgressBarHide();
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


function MultiFile(MultiFilePath) {
    var html = '';
    var split_ = MultiFilePath.split(',');
    for (var i = 0; i < split_.length; i++) {
        if (split_[i] != '') {
            var File = split_[i];
            html += "<a target='_blank'  onclick='var originalTarget = document.forms[0].target; document.forms[0].target = '_blank'; setTimeout(function () { document.forms[0].target = originalTarget; }, 3000);' href=" + File + ">" +
                "<img src='/Images/Attachment.png' style='height: 30px; width: 30px;margin-right: 20px;' />   </a> ";
        }
    }
    return html == "" ? "" : "<strong>Attachments : </strong>" + html;

}

function UploadImage2() {

    ProgressBarShow();
    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {

        var UploadedFilePath = FileUpload(fileUpload);
        if (UploadedFilePath != "") {

         
           // UploadedFilePath = UploadedFilePath.replace(',Image', '');
           // UploadedFilePath = UploadedFilePath.replace(',Word Document', '');
           // UploadedFilePath = UploadedFilePath.replace(',PDF', '');
           // UploadedFilePath = UploadedFilePath.replace(',Excel Spreadsheet', '');
           // UploadedFilePath = UploadedFilePath.replace(',Text File', '');
           // UploadedFilePath = UploadedFilePath.replace(',Rar File', '');
           // UploadedFilePath = UploadedFilePath.replace(',Zip File', '');
           // UploadedFilePath = UploadedFilePath.replace(',Outlook File', '');

           //// var Value_ = UploadedFilePath.split(','); 


            let arr = UploadedFilePath.split(',');

            UploadedFilePath = arr.filter(function (value, index, self) {
                return self.indexOf(value) === index;
            }).join(',');


            let arr1 = UploadedFilePath.split(',');
            chkarray = [];
            $(".hf_FilePath").val('');

            for (let j = 0; j < arr1.length; j++)
            {
                if (arr1[j] !== "") {
                    chkarray.push(arr1[j]);
                    $(".hf_FilePath").val(chkarray.join(','));
                }

            }
            $(".Attachement").val('');
        }

        ProgressBarHide();
    }
}



function UploadImage() {

    ProgressBarShow();

    //  var fileUpload = $('.Attachement').get(0);
    var fileUpload = $('.Attachement').get(0);
    var filesCount = fileUpload.files.length;
    if (filesCount > 0) {
        var UploadedFilePath = FileUpload(fileUpload);
        if (UploadedFilePath != "") {
            var Value_ = UploadedFilePath.split(',');
            chkarray.push(Value_[0]);


            $(".hf_FilePath").val(chkarray.join(','));
            $(".Attachement").val('');

            $('#divTab').text('');
            var tbl_chats = '<table style="border: 1px solid black; border-collapse: collapse;">'
            tbl_chats += '<tr style="width: 906px; border: 1px solid black; border-collapse: collapse;"><th>File Name</th><th>View Document</th>';
            tbl_chats += '</tr>';
            for (let i = 0; i < chkarray.length; i++) {

                tbl_chats += '<tr style="width: 906px; border: 1px solid black; border-collapse: collapse;">';
                tbl_chats += '<td style="border: 1px solid black; border-collapse: collapse;">';
                tbl_chats += chkarray[i];
                tbl_chats += '</td>';
                tbl_chats += '<td style="border: 1px solid black; border-collapse: collapse;">';
                tbl_chats += '<a href=/Uploads/' + chkarray[i] + ' target=_blank><img src="/Images/book-open-icon.png"/></a>';
                tbl_chats += '</td>';
                tbl_chats += '</tr>';
            }
            tbl_chats += '</table>';
            $('#divTab').append(tbl_chats);

            // here
        }
        ProgressBarHide();
    }

}

function ClearFiles() {
    $('#divTab').text('');
    chkarray = [];
    $(".hf_FilePath").val('');
    $('.Attachement').val('');
}


