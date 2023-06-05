<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="TicketReplyRespone.aspx.cs" Inherits="Pages_TicketReplyRespone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/TicketReplyRespone.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>

    <style>
        .BtnAttachmentVisible {
            visibility: visible;
        }

        .BtnAttachmentHidden {
            visibility: hidden;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="hf_ChatTypeId" runat="server" class="hf_ChatTypeId" value="0" />
    <input type="hidden" id="hf_MasterId" runat="server" class="hf_MasterId" value="0" />
    <input type="hidden" id="hfInitiatorId" runat="server" class="hfInitiatorId" value="" />
    <input type="hidden" id="hfAssigneeId" runat="server" class="hfAssigneeId" value="" />
    <input type="hidden" id="hf_UserId" runat="server" class="hf_UserId" value="" />
    <input type="hidden" id="hf_FilePath" runat="server" class="hf_FilePath" value="" />
    <div class="row-fluid DivReplyResponse">
        <div class="content-widgets ">
            <div class="widget-head bluePSW">
                <h3>
                    <asp:Label runat="server" ID="lblHeader" Text="Ticket Reply / Response History"></asp:Label>
                    <button type="button" runat="server" id="lnkbtnTicketDetail" class="btn btn-info pull-right lnkbtnTicketDetail" style="margin-top: 3px; margin-right: 3px;">Ticket Details</button>
                </h3>
            </div>


            <div class="widget-container">

                <table class="table" style="margin-bottom: 0px !important">
                    <thead>
                        <tr>
                            <td>
                                <img src="/images/user-pro.png" class="profile_pic circle" style="width: 50px; height: 50px; margin-left: 5px;" />
                            </td>
                            <td>
                                <div id="DivTxt" runat="server" class="chatbaloon color_gray baloon_arrow DivTxt">
                                    <div class="arrow_right"></div>
                                    <cc1:Editor ID="txtDescription" Style="border: 1px solid; border-color: #d4d4d4;" runat="server" CssClass="txtDescription RichBoxValidate" targetcontrolid="txtDescription"></cc1:Editor>
                                </div>
                                <div style="margin-left: 30px;" class="Attachement_div">
                                    Note : <span style="color: red">(Only 5 mb size files are allowed. File Name must not contain brackets.)</span>
                                    <br />
                                    <strong>Attachment :</strong>  &nbsp
                                    <input type="file" class="form-control Attachement" onchange="ShowImage(this);"  multiple="multiple"/>
                                    <input type="button" id="Upload" class="btn btn-danger Upload Btn_UploadDiv" value="Upload" />
                                    <input type="button" id="Clear" class="btn btn-danger Clear" value="Clear" />
                                    <br />
                                    <strong>
                                        <a href="#" class="Clearfileupload" style="color: blue">Clear Attachment</a>
                                    </strong>
                                </div>
                                <div id="divTab"></div>
                                <span style="float: right; padding-right: 3%; margin-top: 5px">
                                    <asp:Button ID="btn_Save" runat="server" title="Reply" value="Save" class="btn btn-danger btn_Save" />
                                </span>
                            </td>
                        </tr>
                    </thead>

                    <tbody class="wfForm">
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    

    <script type="text/x-jQuery-tmpl" id="wfForm">
        <tr class="tr_">
            <td>
                <img src="/images/user-pro.png" class="profile_pic circle" style="width: 50px; height: 50px; margin-left: 5px;" />
            </td>
            <td>
                <strong><span style="padding-left: 3%">${ActionBy}</span></strong>
                <strong><span style="float: right; padding-right: 3%">${Date}</span></strong>
                <div class="chatbaloon baloon_arrow ${IsReply == 1 ? 'color_gray' : 'color_bluePSW'} arrow" style="min-height: 30px; max-height: 400px; overflow-y: scroll">
                   <%-- <div class="${IsReply == 1 ? 'arrow_right' : 'arrow'}"></div>--%>
                    <span>
                        <label style="white-space: pre-line; width: 500px;">{{html Description}}</label>
                    </span>
                </div>
                <span style="padding-left: 3%">{{html MultiFile(FilePath)}} 
                </span>

            </td>
        </tr>
    </script>

    <div id="myModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="left: 5%; width: 90%; height: 90%; margin-left: 0px; top: 3%; background-color: transparent">
        <div class="modal-dialog" style="height: 100%;">
            <div class="pull-right" style="margin-right: -10px">
                <input id="btnClosePoP" type="image" src="/Images/close-button.png" class="btnClosePoP" />
            </div>
            <iframe src="" style="height: 100%; width: 100%;"></iframe>
        </div>
    </div>

    <script type="text/javascript">

        function ShowImage(input) {
            var Filesize = 5145728;
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpeg|.jpg|.png|.gif|.png|.gif|.bmp|.pdf|.xls|.xlsx|.doc|.docx|.txt)$/;
            if (input.files && input.files[0]) {
                $('#imgCapture').attr('src', null);
                var file = input.files[0];
                if (regex.test(file.name.toLowerCase())) {

                    if (input.files[0].size <= Filesize) {

                    }
                    else {
                        $(".Attachement").val('');
                        AlertBox('Error!', "Maximum file size is 5 mb", 'error');
                        return false;
                    }

                } else {
                    $(".Attachement").val('');
                    AlertBox('Error!', "" + file.name + ' is not a valid file.', 'error');
                    return false;
                }
            }
        }

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

        function pageLoad() {
            $(".Clearfileupload").click(function () {
                $('.Attachement').val('');
            });
            Get_Reply_Response();



            OnLoad();

            $(".btn_Save").click(function () {
                UploadImage2();
                Save();
                return false;
            });


            $(".lnkbtnTicketDetail").click(function () {
                var TicketMasterId = $('.hf_MasterId').val();
                var Url = "/pages/InitiateTicketsDetails.aspx?ControlFalse=False&&hf_IsInitiator=True&&TMID=" + TicketMasterId + "";
                openPopupFancy(Url);
            });

            $(".btnClosePoP").click(function () {
                IframeClosePop();
                $('.btn_Search').click();
                return false;
            });

            $(".Upload").click(function () {
                UploadImage2();
            });

            $(".Clear").click(function () {
                ClearFiles();
            });

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


        function IframePop(url) {
            $('#myModal').zIndex(1050);
            $('.modal').find("iframe").attr("src", url);
        }
        function IframeClosePop() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();

        }

        function ControlsValidateFunction() {
            var State = true;
            $(".RichBoxValidate").each(function () {
                var InnerText = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content();
                if (InnerText == "") {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border", "1px solid");
                    $(this).css("border-color", "#d4d4d4");
                }
            });
            return State;
        }

        //window.setInterval(function () {
        //    Get_Reply_Response();
        //}, 5000);

    </script>

</asp:Content>

