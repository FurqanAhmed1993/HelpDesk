<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="Meeting.aspx.cs" Inherits="Pages_Meeting" %>

<%@ Register Src="~/Controls/Shared/MultipleSelectionBox.ascx" TagPrefix="uc1" TagName="MultipleSelectionBox" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="/js/Pages_JS/Meeting.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <div class="container-fluid">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

                <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
                <input type="hidden" id="hf_View" runat="server" class="hf_View" value="0" />
                <input type="hidden" id="hf_TicketMeetingId" runat="server" class="hf_TicketMeetingId" value="0" />
                <div class="row-fluid">
                    <div class="content-widgets">
                        <div class="widget-head orange div_Header">
                            <h3>Meeting
                                <%--<asp:Label runat="server" ID="lblHeading" CssClass="lblHeading" Text="Meeting"></asp:Label>--%>
                                <%--<label class="lblHeading" title="Meeting"></label>--%>
                            </h3>


                        </div>
                        <div class="widget-container">
                            <div class="controls-row">
                                <div class="control-group span10">
                                    <label class="label-bold">Meeting Agenda</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtMeetingAgenda" placeholder="Meeting Agenda" CssClass="span12  txtMeetingAgenda TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span1 DivBtnMinutesofMeeting">
                                    <label class="label-bold" style="color: white">.</label>
                                    <div class="controls">
                                        <asp:Button runat="server" ID="btnMinutesofMeeting" Text="Minutes of Meeting" data-toggle="modal" data-target="#myModal" class="myModal btn btn-danger btnMinutesofMeeting"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span10">
                                    <label class="label-bold">Location</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtLocation" placeholder="Location" CssClass="span12 txtLocation TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Date</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtDate" CssClass="span12 datePicker txtDate TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Start Time</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlHrsFrom" runat="server" CssClass="span12 ddlHrsFrom  DropDownValidate" Width="55px">
                                            <asp:ListItem Text="Hrs" Value="0" />
                                            <asp:ListItem Text="00" Value="00" />
                                            <asp:ListItem Text="01" Value="01" />
                                            <asp:ListItem Text="02" Value="02" />
                                            <asp:ListItem Text="03" Value="03" />
                                            <asp:ListItem Text="04" Value="04" />
                                            <asp:ListItem Text="05" Value="05" />
                                            <asp:ListItem Text="06" Value="06" />
                                            <asp:ListItem Text="07" Value="07" />
                                            <asp:ListItem Text="08" Value="08" />
                                            <asp:ListItem Text="09" Value="09" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                            <asp:ListItem Text="13" Value="13" />
                                            <asp:ListItem Text="14" Value="14" />
                                            <asp:ListItem Text="15" Value="15" />
                                            <asp:ListItem Text="16" Value="16" />
                                            <asp:ListItem Text="17" Value="17" />
                                            <asp:ListItem Text="18" Value="18" />
                                            <asp:ListItem Text="19" Value="19" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="21" Value="21" />
                                            <asp:ListItem Text="22" Value="22" />
                                            <asp:ListItem Text="23" Value="23" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMinFrom" runat="server" CssClass="span12  ddlMinFrom DropDownValidate" Width="55px">
                                            <asp:ListItem Text="Min" Value="0" />
                                            <asp:ListItem Text="00" Value="00" />
                                            <asp:ListItem Text="01" Value="01" />
                                            <asp:ListItem Text="02" Value="02" />
                                            <asp:ListItem Text="03" Value="03" />
                                            <asp:ListItem Text="04" Value="04" />
                                            <asp:ListItem Text="05" Value="05" />
                                            <asp:ListItem Text="06" Value="06" />
                                            <asp:ListItem Text="07" Value="07" />
                                            <asp:ListItem Text="08" Value="08" />
                                            <asp:ListItem Text="09" Value="09" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                            <asp:ListItem Text="13" Value="13" />
                                            <asp:ListItem Text="14" Value="14" />
                                            <asp:ListItem Text="15" Value="15" />
                                            <asp:ListItem Text="16" Value="16" />
                                            <asp:ListItem Text="17" Value="17" />
                                            <asp:ListItem Text="18" Value="18" />
                                            <asp:ListItem Text="19" Value="19" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="21" Value="21" />
                                            <asp:ListItem Text="22" Value="22" />
                                            <asp:ListItem Text="23" Value="23" />
                                            <asp:ListItem Text="24" Value="24" />
                                            <asp:ListItem Text="25" Value="25" />
                                            <asp:ListItem Text="26" Value="26" />
                                            <asp:ListItem Text="27" Value="27" />
                                            <asp:ListItem Text="28" Value="28" />
                                            <asp:ListItem Text="29" Value="29" />
                                            <asp:ListItem Text="30" Value="30" />
                                            <asp:ListItem Text="31" Value="31" />
                                            <asp:ListItem Text="32" Value="32" />
                                            <asp:ListItem Text="33" Value="33" />
                                            <asp:ListItem Text="34" Value="34" />
                                            <asp:ListItem Text="35" Value="35" />
                                            <asp:ListItem Text="36" Value="36" />
                                            <asp:ListItem Text="37" Value="37" />
                                            <asp:ListItem Text="38" Value="38" />
                                            <asp:ListItem Text="39" Value="39" />
                                            <asp:ListItem Text="40" Value="40" />
                                            <asp:ListItem Text="41" Value="41" />
                                            <asp:ListItem Text="42" Value="42" />
                                            <asp:ListItem Text="43" Value="43" />
                                            <asp:ListItem Text="44" Value="44" />
                                            <asp:ListItem Text="45" Value="45" />
                                            <asp:ListItem Text="46" Value="46" />
                                            <asp:ListItem Text="47" Value="47" />
                                            <asp:ListItem Text="48" Value="48" />
                                            <asp:ListItem Text="49" Value="49" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="51" Value="51" />
                                            <asp:ListItem Text="52" Value="52" />
                                            <asp:ListItem Text="53" Value="53" />
                                            <asp:ListItem Text="54" Value="54" />
                                            <asp:ListItem Text="55" Value="55" />
                                            <asp:ListItem Text="56" Value="56" />
                                            <asp:ListItem Text="57" Value="57" />
                                            <asp:ListItem Text="58" Value="58" />
                                            <asp:ListItem Text="59" Value="59" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">End Time</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlHRsTo" runat="server" CssClass="span12 ddlHRsTo  DropDownValidate" Width="55px">
                                            <asp:ListItem Text="Hrs" Value="0" />
                                            <asp:ListItem Text="00" Value="00" />
                                            <asp:ListItem Text="01" Value="01" />
                                            <asp:ListItem Text="02" Value="02" />
                                            <asp:ListItem Text="03" Value="03" />
                                            <asp:ListItem Text="04" Value="04" />
                                            <asp:ListItem Text="05" Value="05" />
                                            <asp:ListItem Text="06" Value="06" />
                                            <asp:ListItem Text="07" Value="07" />
                                            <asp:ListItem Text="08" Value="08" />
                                            <asp:ListItem Text="09" Value="09" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                            <asp:ListItem Text="13" Value="13" />
                                            <asp:ListItem Text="14" Value="14" />
                                            <asp:ListItem Text="15" Value="15" />
                                            <asp:ListItem Text="16" Value="16" />
                                            <asp:ListItem Text="17" Value="17" />
                                            <asp:ListItem Text="18" Value="18" />
                                            <asp:ListItem Text="19" Value="19" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="21" Value="21" />
                                            <asp:ListItem Text="22" Value="22" />
                                            <asp:ListItem Text="23" Value="23" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMinTo" runat="server" CssClass="span12 ddlMinTo  DropDownValidate" Width="55px">
                                            <asp:ListItem Text="Min" Value="0" />
                                            <asp:ListItem Text="00" Value="00" />
                                            <asp:ListItem Text="01" Value="01" />
                                            <asp:ListItem Text="02" Value="02" />
                                            <asp:ListItem Text="03" Value="03" />
                                            <asp:ListItem Text="04" Value="04" />
                                            <asp:ListItem Text="05" Value="05" />
                                            <asp:ListItem Text="06" Value="06" />
                                            <asp:ListItem Text="07" Value="07" />
                                            <asp:ListItem Text="08" Value="08" />
                                            <asp:ListItem Text="09" Value="09" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                            <asp:ListItem Text="13" Value="13" />
                                            <asp:ListItem Text="14" Value="14" />
                                            <asp:ListItem Text="15" Value="15" />
                                            <asp:ListItem Text="16" Value="16" />
                                            <asp:ListItem Text="17" Value="17" />
                                            <asp:ListItem Text="18" Value="18" />
                                            <asp:ListItem Text="19" Value="19" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="21" Value="21" />
                                            <asp:ListItem Text="22" Value="22" />
                                            <asp:ListItem Text="23" Value="23" />
                                            <asp:ListItem Text="24" Value="24" />
                                            <asp:ListItem Text="25" Value="25" />
                                            <asp:ListItem Text="26" Value="26" />
                                            <asp:ListItem Text="27" Value="27" />
                                            <asp:ListItem Text="28" Value="28" />
                                            <asp:ListItem Text="29" Value="29" />
                                            <asp:ListItem Text="30" Value="30" />
                                            <asp:ListItem Text="31" Value="31" />
                                            <asp:ListItem Text="32" Value="32" />
                                            <asp:ListItem Text="33" Value="33" />
                                            <asp:ListItem Text="34" Value="34" />
                                            <asp:ListItem Text="35" Value="35" />
                                            <asp:ListItem Text="36" Value="36" />
                                            <asp:ListItem Text="37" Value="37" />
                                            <asp:ListItem Text="38" Value="38" />
                                            <asp:ListItem Text="39" Value="39" />
                                            <asp:ListItem Text="40" Value="40" />
                                            <asp:ListItem Text="41" Value="41" />
                                            <asp:ListItem Text="42" Value="42" />
                                            <asp:ListItem Text="43" Value="43" />
                                            <asp:ListItem Text="44" Value="44" />
                                            <asp:ListItem Text="45" Value="45" />
                                            <asp:ListItem Text="46" Value="46" />
                                            <asp:ListItem Text="47" Value="47" />
                                            <asp:ListItem Text="48" Value="48" />
                                            <asp:ListItem Text="49" Value="49" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="51" Value="51" />
                                            <asp:ListItem Text="52" Value="52" />
                                            <asp:ListItem Text="53" Value="53" />
                                            <asp:ListItem Text="54" Value="54" />
                                            <asp:ListItem Text="55" Value="55" />
                                            <asp:ListItem Text="56" Value="56" />
                                            <asp:ListItem Text="57" Value="57" />
                                            <asp:ListItem Text="58" Value="58" />
                                            <asp:ListItem Text="59" Value="59" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Description</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="txtDescription" Height="100px" TextMode="MultiLine" Width="98.5%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>





                            <div class="controls-row Div_AttendeeSelecttion">
                                <div class="widget-head orange">
                                    <h3>Attendee</h3>
                                </div>
                                <div class="control-group span12">
                                    <label class="label-bold"></label>
                                    <div class="controls">
                                        <uc1:MultipleSelectionBox runat="server" ID="MultipleSelectionBox1" DestinationHeadingText="Selected Attendee(s)" SourceHeadingText="Attendee(s)" />
                                    </div>
                                </div>
                            </div>




                            <div class="controls-row Div_AttendeeView">
                                <div class="widget-head orange">
                                    <h3>Attendees</h3>
                                </div>
                                <div id="div_AttendeeList" class="control-group span12 div_AttendeeList">
                                </div>
                            </div>

                            <div class="controls-row divAttachement ">
                                <div class="widget-head orange">
                                    <h3>Attachment</h3>
                                </div>

                                <div class="control-group span12 Attchemntupload">
                                    <label class="label-bold">
                                    </label>
                                    <div class="controls">
                                        <input type="file" class="form-control Attachement" />
                                        <input type="button" id="Upload" class="btn btn-danger Upload Btn_UploadDiv" value="Upload" style="visibility: hidden" />
                                    </div>
                                </div>

                                <div class="AttchemntRepeter">
                                    <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                        <thead>
                                            <tr>
                                                <th style="width: 60%">File Name</th>
                                                <th style="width: 20%">File Type</th>
                                                <th>View</th>
                                                <th>Download</th>
                                                <%--  <th>Delete</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody class="wfattachment">
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="controls-row Div_MinutsofMeeting">
                                <div class="widget-head orange">
                                    <h3>Minute(s) of Meeting</h3>
                                </div>
                                <asp:TextBox ID="txtMinutesOfMeeting" runat="server" CssClass="txtMinutesOfMeeting" Height="100px" TextMode="MultiLine" Width="98.75%"></asp:TextBox>
                            </div>


                            <div class="controls-row Div_ButttonSave">
                                <div class="pull-right">
                                    <asp:Button runat="server" ID="btn_Save" Text="Save" ValidationGroup="ValidateOnSave" class="btn btn-danger btn_Save"></asp:Button>
                                </div>
                            </div>

                        </div>


                    </div>


                </div>

            </ContentTemplate>
        </asp:UpdatePanel>



        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">

                    <div class="modal-body">
                        <div class="row-fluid">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <input type="hidden" id="hfId" runat="server" class="hfId" />
                                    <div class="content-widgets light-gray">
                                        <div class="widget-head orange">
                                            <h3>Minutes Of Meeting</h3>
                                        </div>
                                        <div class="widget-container">
                                            <div class="controls-row">
                                                <div class="control-group span12">
                                                    <label class="label-bold"></label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtAddMinutesOfMeeting" runat="server" CssClass="txtAddMinutesOfMeeting" Height="200px" TextMode="MultiLine" Width="98.75%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="controls-row" style="margin-bottom: 20px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_SaveMinutesOfMeeting" Text="Save" class="btn btn-danger btn_SaveMinutesOfMeeting"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_ClosePopUp" Text="Close" class="btn btn-warning btn_ClosePopUp"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </div>




    </div>


    <script type="text/x-jQuery-tmpl" id="wfattachment">
        <tr>
            <td>
                <asp:Label runat="server" ID="lbl" Text="${FileOriginalName}"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label runat="server" ID="Label4" Text="${Filetype}"></asp:Label>
            </td>

            <td style="text-align: center">
                <a target="_blank" onclick="var originalTarget = document.forms[0].target; document.forms[0].target = '_blank'; setTimeout(function () { document.forms[0].target = originalTarget; }, 3000);" href="${FilePath}">
                    <img src="/Images/book-open-icon.png" />
                </a>
            </td>
            <td style="text-align: center">
                <a target="_blank" href="${FilePath}" download>
                    <img src="/images/downld.gif" />
                </a>
            </td>
            <%--   <td style="text-align: center">
                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="ImageDelete(${FileId})" ImageUrl="/images/deletefile.gif" />
            </td>--%>
        </tr>
    </script>

    <script type="text/javascript">

        function pageLoad() {
          
            var pickerOpts = {
                changeMonth: true,
                changeYear: true,
                yearRange: "-100: +10",
                dateFormat: "M dd,yy"
            };
            $('.datePicker').datepicker(pickerOpts);
            $('.datePicker').keydown(function () {
                return false;
            });


            OnLoad();
            $(".btn_Save").click(function () {
                Save();
                return false;
            });


            $(".btnMinutesofMeeting").click(function () {
                OpenPopup();
            });



            $(".btn_ClosePopUp").click(function () {
                ClosePopup();
                return false;
            });

            $(".btn_SaveMinutesOfMeeting").click(function () {
                SaveMinutesOfMeeting();
                return false;
            });



        }



        function ClosePopup() {
            $(".txtAddMinutesOfMeeting").val("");
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function OpenPopup() {
            $(".txtAddMinutesOfMeeting").val("");
            $('#myModal').zIndex(1050);

        }




        //function ImageDelete(id) {
        //    DeleteAtachment(id);
        //}

        function ControlsValidateFunction() {

            var State = true;
            $(".DropDownValidate").each(function () {
                if ($(this).val() == null) {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else if ($(this).val() == "0") {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }

            });
            $(".TextBoxValidate").each(function () {

                if ($(this).val() == "") {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }
            });

            if (State == true) {
                var HasValue = false;
                $('.ListBoxDestination option').each(function () {
                    var Response = new Object();
                    if (HasValue == false) {
                        var Id = $(this).val();
                        if (Id != null) {
                            HasValue = true;
                        } else if (Id != "") {
                            HasValue = true;
                        }
                    }
                });

                if (HasValue == true) {
                    State = true;
                }
                else {
                    State = false;
                    AlertBox("Alert", "In order to create meeting kindly select attendee", "warning");
                }
            }


            return State;
        }



        function ControlsValidateMinutsOfMinuts() {

            var State = true;
            $(".txtAddMinutesOfMeeting").each(function () {

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




    </script>





</asp:Content>

