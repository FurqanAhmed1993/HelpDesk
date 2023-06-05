<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="TaskDetail.aspx.cs" Inherits="Pages_TaskDetail" %>

<%@ Register Src="~/Controls/Shared/WFStatus.ascx" TagPrefix="uc1" TagName="WFStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/TaskDetail.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <input type="hidden" id="hfAssigneeId" runat="server" class="hfAssigneeId" value="0" />
                <input type="hidden" id="hfTaskMasterId" runat="server" class="hfTaskMasterId" value="0" />
                <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
                <input type="hidden" id="hf_IsInitiator" runat="server" class="hf_IsInitiator" value="0" />
                <input type="hidden" id="hf_UserId" runat="server" class="hf_UserId" value="0" />
                <input type="hidden" id="hf_DepartmentId" runat="server" class="hf_DepartmentId" value="0" />
                <input type="hidden" id="hf_IsSuperAdmin" runat="server" class="hf_IsSuperAdmin" value="false" />
                <input type="hidden" id="hf_IsAdmin" runat="server" class="hf_IsAdmin" value="false" />
                <input type="hidden" id="hf_IsIncharge" runat="server" class="hf_IsIncharge" value="false" />

                <div class="row-fluid">
                    <div class="content-widgets">
                        <div class="widget-head orange">
                            <h3>Task History  </h3>
                        </div>
                        <div class="widget-container" style="border: 1px solid">
                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Initiator</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblInitiator" CssClass="span12 lblInitiator"></asp:Label>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="label-bold">Date</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblDate" CssClass="span12 lblDate"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Time</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblTime" CssClass="span12 lblTime"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Status</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblStatus" Visible="false" CssClass="span12 lblStatus"></asp:Label>
                                        <div id="dvTicketStatusPopup" class="dvTicketStatusPopup ">
                                            <uc1:WFStatus runat="server" ID="WFStatus" Visible="true" IsTicketStatus="false" />
                                        </div>
                                        <div class='manageCategory DivShowDetail'>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Priority  </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblPriority" CssClass="span12 lblPriority"></asp:Label>
                                    </div>
                                </div>



                                <div class="control-group span3">
                                    <label class="label-bold">Start Time</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblStartTime" CssClass="span12 lblStartTime"></asp:Label>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="label-bold">End Time</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblEndTime" CssClass="span12 lblEndTime"></asp:Label>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="label-bold">Assignee</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblAssignee" CssClass="span12 lblAssignee"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row DivAssignee">
                                <div class="control-group span6">
                                    <label class="label-bold">Assignee</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAssignee" CssClass="span6 ddlAssignee ValidateAssignee"></asp:DropDownList>
                                        <asp:Button runat="server" ID="btn_UpdateAssignee" Text="Assign" class="span2 btn btn-danger btn_UpdateAssignee"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Title </label>
                                    <div class="controls chatbaloon Title" id="Title" style="min-height: 20px; width: 99%; display: table-caption">
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Initial findings  </label>

                                    <div class="controls chatbaloon" style="min-height: 60px; width: 99%">
                                        <asp:TextBox ID="Description" runat="server" CssClass="Description" Height="100px" ReadOnly="true" TextMode="MultiLine" Width="98.5%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>



                        <div class="controls-row" style="border: 1px solid">
                            <div class="widget-head orange">
                                <h3>Attachment</h3>
                            </div>



                            <div class="control-group span12">
                                <label class="label-bold">
                                </label>
                                <div class="controls">
                                    <input type="file" class="form-control Attachement" />
                                    <input type="button" id="Upload" class="btn btn-danger Upload Btn_UploadDiv" value="Upload" />
                                </div>
                            </div>


                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                <thead>
                                    <tr>
                                        <th style="width: 60%">File Name</th>
                                        <th style="width: 20%">File Type</th>
                                        <th>View</th>
                                        <th>Download</th>
                                        <%-- <th>Delete</th>--%>
                                    </tr>
                                </thead>
                                <tbody class="wfattachment">
                                </tbody>
                            </table>
                        </div>
                        <div class="controls-row" style="border: 1px solid" id="DivStatus">
                            <div class="widget-head orange">
                                <h3>Change Status</h3>
                            </div>
                            <div class="widget-container">
                                <div class="controls-row">
                                    <div class="control-group span3">
                                        <label class="label-bold">Status </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="span12 ddlStatus DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="controls-row">
                                    <div class="control-group span12">
                                        <label class="label-bold">Findings </label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDescription" runat="server" placeHolder="Findings " TextMode="MultiLine" CssClass="span12 txtDescription"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="controls-row">
                                    <div class="pull-right">
                                        <asp:Button runat="server" ID="btn_Save" Text="Update Stataus" class="btn btn-danger btn_Save"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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
            <%--<td style="text-align: center">
                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="ImageDelete(${FileId})" ImageUrl="/images/deletefile.gif" />
            </td>--%>
        </tr>
    </script>

    <script type="text/javascript">

        function pageLoad() {

            OnLoad();
            $(".Upload").click(function () {
                UploadImage();
            });


            $(".btn_Save").click(function () {
                Save();
            });


            $(".btn_UpdateAssignee").click(function () {
                UpdateAssignee();
            });
            $('.dvTicketStatusPopup').click(function () {
                $('.DivShowDetail').html($(this).find(".WFStatusPanel").html());
                ShowPopupDiv();
            });

        }

        function ShowPopupDiv() {
            $('.manageCategory').not(':last').remove();
            $('.DivShowDetail').dialog({
                modal: true,
                width: '60%',
                resizable: false,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        }


        function ShowPopupDivResponse() {
            $('.DivShowReponse').dialog({
                modal: true,
                width: '80%',
                resizable: false,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
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

            return State;
        }

        function ControlValidateAssignee() {
            var State = true;
            $(".ValidateAssignee").each(function () {
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

            return State;
        }



    </script>


</asp:Content>

