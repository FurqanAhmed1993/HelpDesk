<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="Task.aspx.cs" Inherits="Pages_Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/Task.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                 <input type="hidden" id="hf_UserId" runat="server" class="hf_UserId" value="0" />
                <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
                <input type="hidden" id="hf_View" runat="server" class="hf_View" value="0" />
                <input type="hidden" id="hfTaskMasterId" runat="server" class="hfTaskMasterId" value="0" />
                <input type="hidden" id="hfIsInitiator" runat="server" class="hfIsInitiator" value="0" />
                



                <div class="row-fluid">
                    <div class="content-widgets">
                        <div class="widget-head orange div_Header">
                            <h3>Task
                            </h3>


                        </div>
                        <div class="widget-container">
                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Title</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtTitle" placeholder="Title" CssClass="span12  txtTitle TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Initiator </label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlInitiator" CssClass="span12 ddlInitiator DropDownValidate"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">

                                <div class="control-group span3">
                                    <label class="label-bold">Department </label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="span12 ddlDepartment DropDownValidate"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="label-bold">
                                        Assignee To
                                        <asp:Label ID="Label1" runat="server" Text="(Optional)" ForeColor="Red"></asp:Label>
                                    </label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAssignee" CssClass="span12 ddlAssignee"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="label-bold">Start Date</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txStarttDate" CssClass="span12 datePicker txStarttDate TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">End Date</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtEndDate" CssClass="span12 datePicker txtEndDate TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>


                            </div>

                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Priority  </label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlPriority" CssClass="span12 ddlPriority DropDownValidate"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Initial findings </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDescription" runat="server" placeholder="Initial Findings" CssClass="txtDescription TextBoxValidate" Height="100px" TextMode="MultiLine" Width="98.5%"></asp:TextBox>
                                    </div>
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

            return State;
        }







    </script>



</asp:Content>

