<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="InitiateTicketsDetails.aspx.cs" Inherits="Pages_InitiateTicketsDetails" %>


<%--<%@ Register Src="~/Controls/Shared/WFStatus.ascx" TagPrefix="uc1" TagName="WFStatus" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/InitiateTicketsDetails.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input type="hidden" id="hf_CloseTicketStatus" runat="server" class="hf_CloseTicketStatus" value="0" />
                <input type="hidden" id="hfEmailProductId" runat="server" class="hfEmailProductId" value="0" />
                <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
                <input type="hidden" id="hf_IsInitiator" runat="server" class="hf_IsInitiator" value="0" />
                <input type="hidden" id="hf_UserId" runat="server" class="hf_UserId" value="0" />
                <input type="hidden" id="hf_DepartmentId" runat="server" class="hf_DepartmentId" value="0" />
                <input type="hidden" id="hf_IsSuperAdmin" runat="server" class="hf_IsSuperAdmin" value="false" />
                <input type="hidden" id="hf_IsAdmin" runat="server" class="hf_IsAdmin" value="false" />
                <input type="hidden" id="hf_IsIncharge" runat="server" class="hf_IsIncharge" value="false" />
                <input type="hidden" id="hf_StatusId" runat="server" class="hf_StatusId" value="0" />
                <input type="hidden" id="hf_AssigneeId" runat="server" class="hf_AssigneeId" value="0" />
                <input type="hidden" id="hf_RoleCode" runat="server" class="hf_RoleCode" value="0" />
                <input type="hidden" id="hf_TypeOfIssueId" runat="server" class="hf_TypeOfIssueId" value="0" />
                <input type="hidden" id="hf_ProductSubCategoryId" runat="server" class="hf_ProductSubCategoryId" value="0" />

                <div class="row-fluid">
                    <div class="content-widgets">
                        <div class="widget-head bluePSW">
                            <h3>Ticket History  </h3>
                        </div>
                        <div class="widget-container" style="border: 1px solid">
                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Ticket No </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblTicketNo" CssClass="span12 lblTicketNo"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Status</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblStatus" CssClass="span12 lblStatus"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Method Of Contact </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblMethodOfContact" CssClass="span12 lblMethodOfContact"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Priority </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblPriority" CssClass="span12 lblPriority"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="controls-row">


                                <div class="control-group span3">
                                    <label class="label-bold">Initiator</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblInitiator" CssClass="span12 lblInitiator"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Department </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lbl_Department" CssClass="span12 lbl_Department"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Assignee</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lbl_Assignee" CssClass="span12 lbl_Assignee"></asp:Label>
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Customer Name</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblName" CssClass="span12 lblName"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Customer Contact</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblContact" CssClass="span12 lblContact"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Customer Email</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblEmail" CssClass="span12 lblEmail"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Customer City</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblCity" CssClass="span12 lblCity"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Customer Address</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblAddress" CssClass="span12 lblAddress"></asp:Label>
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Request Type</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblRequestType" CssClass="span12 lblRequestType"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Category </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lbl_Category" CssClass="span12 lbl_Category"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Subcategory </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblSubcategory" CssClass="span12 lblSubcategory"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Alternative Number</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblAltNumber" CssClass="span12 lblAltNumber"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="dynamicFields" id="dynamicFields">
                            </div>

                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="label-bold">Product </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblTypeOfIssue" CssClass="span12 lblTypeOfIssue"></asp:Label>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="label-bold">Product Sub-Category </label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblProductSubCategory" CssClass="span12 lblProductSubCategory"></asp:Label>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="label-bold">Findings</label>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblTypeOfComplaint" CssClass="span12 lblTypeOfComplaint"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Title </label>
                                    <div class="controls chatbaloon Title" id="Title" style="min-height: 20px; width: 99%">
                                    </div>
                                </div>
                            </div>



                            <div class="controls-row">
                                <div class="control-group span12">
                                    <label class="label-bold">Initial findings </label>
                                    <div style="min-height: 100px; max-height: 200px; overflow-y: scroll" class="chatbaloon">
                                        <asp:Label runat="server" ID="Description" Style="white-space: pre-wrap;" CssClass="Description" Width="98.5%" />
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row Div_To">
                                <div class="control-group span12">
                                    <label class="label-bold">
                                        Email To
                                        
                                        <asp:Label ID="lblmessage" runat="server" Text="(Note : Enter semicolon separated email address without spaces)" ForeColor="Red"></asp:Label>
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtTo" placeholder="Email To" CssClass="span11 txtToEmail"></asp:TextBox>
                                        <div class="pull-right">
                                            <%-- <asp:Button runat="server" ID="btn_UpdateToEmail" Text="Update" class="btn btn-info btn_UpdateToEmail HideAll"></asp:Button>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row Div_CC">
                                <div class="control-group span12">
                                    <label class="label-bold">
                                        Email CC 
                                       
                                        <asp:Label ID="Label3" runat="server" Text="(Note : Enter semicolon separated email address without spaces)" ForeColor="Red"></asp:Label>
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtCC" placeholder="Email CC" CssClass="span11 txtCCEmail"></asp:TextBox>
                                        <div class="pull-right">
                                            <%-- <asp:Button runat="server" ID="btn_UpdateCCEmail" Text="Update" class="btn btn-info btn_UpdateCCEmail HideAll"></asp:Button>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </div>

                        <div class="controls-row AttachmentControl" style="border: 1px solid">
                            <div class="widget-head bluePSW">
                                <h3>Attachment</h3>
                            </div>



                            <div class="control-group span12 Btn_UploadDiv">
                                <label class="label-bold">
                                </label>
                                <div class="controls">
                                    <input type="file" class="form-control Attachement" />
                                    <input type="button" id="Upload" class="btn btn-danger Upload HideAll" value="Upload" />
                                </div>
                            </div>


                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                <thead>
                                    <tr>
                                        <th style="width: 60%">File Name</th>
                                        <%-- <th style="width: 20%">File Type</th>--%>
                                        <th style="text-align: center">View</th>
                                        <th style="text-align: center">Download</th>
                                        <th style="text-align: center">Delete</th>
                                    </tr>
                                </thead>
                                <tbody class="wfattachment">
                                </tbody>
                            </table>
                        </div>

                        <div class="controls-row DivStatus" style="border: 1px solid" id="DivStatus">
                            <div class="widget-head bluePSW">
                                <h3>Change Status</h3>
                            </div>
                            <div class="widget-container">

                                <div class="controls-row">
                                    <div class="control-group span3">
                                        <label class="label-bold">Department<span style="color: #ff0000">*</span> </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="span12 ddlDepartment DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group span3">
                                        <label class="label-bold">Assignee</label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlAssignee" CssClass="span12 ddlAssignee"></asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                                <div class="controls-row">
                                    <div class="control-group span3">
                                        <label class="label-bold">Status<span style="color: #ff0000">*</span> </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="span12 ddlStatus"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group span3">
                                        <label class="label-bold">Product<span style="color: #ff0000">*</span> </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlTypeOfIssue" CssClass="span12 ddlTypeOfIssue"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="control-group span3">
                                        <label class="label-bold">Product Sub-Category<span style="color: #ff0000">*</span> </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlProductSubCategory" CssClass="span12 ddlProductSubCategory"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="control-group span3 TypeOfComplaint">
                                        <label class="label-bold">Findings<span style="color: #ff0000">*</span></label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlTypeOfComplaint" CssClass="span12 ddlTypeOfComplaint"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="controls-row">
                                    <%--div_Desc--%>
                                    <div class="control-group span12">
                                        <label class="label-bold">Incident Description<span style="color: #ff0000">*</span></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDescription" runat="server" placeHolder="Description..." CssClass="txtDescription" Height="100px" TextMode="MultiLine" Width="98.5%" Style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="controls-row">
                                    <div class="pull-right">
                                        <asp:Button runat="server" ID="btn_Save" Text="Update" class="btn btn-danger btn_Save HideAll"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="controls-row div_ShowCancelRemarks" runat="server" visible="false" id="div_ShowCancelRemarks">
                            <div class="control-group span12">
                                <label class="label-bold">Other Finding</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtShowCancelRemarks" runat="server" Enabled="false" Style="resize: none;" placeHolder="Other Finding" CssClass="txtShowCancelRemarks" Height="100px" TextMode="MultiLine" Width="98.5%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>



    <script type="text/x-jQuery-tmpl" id="wfForm">

        <tr class="tr_">
            <td>
                <%--   <input type="text" disabled="disabled" class="POCName" value="${POCName}" />--%>
                <label class="POCName">${POCName}</label>
            </td>
            <td>
                <%--     <input type="text" disabled="disabled" class="POCContact" value="${POCContact}" />--%>
                <label class="POCContact">${POCContact}</label>

            </td>
            <td>
                <%--<input type="email" disabled="disabled" class="POCEmail" value="${POCEmail}" />--%>
                <label class="POCEmail">${POCEmail}</label>
            </td>
        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="wfattachment">
        <tr class="tr_attachment">
            <td>
                <asp:Label runat="server" ID="lbl" Text="${FileOriginalName}"></asp:Label>
            </td>
            <%-- <td style="text-align: center">
                <asp:Label runat="server" ID="Label4" Text="${Filetype}"></asp:Label>
            </td>--%>

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
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton1" CssClass="btn_DeleteImage" runat="server" OnClientClick="ImageDelete(${FileId})" ImageUrl="/images/deletefile.gif" />
            </td>
        </tr>
    </script>

    <script type="text/javascript">

        function pageLoad() {

            $('.txtToEmail').on('keypress', function (e) {
                if (e.which == 32)
                    return false;
            });

            $('.txtCCEmail').on('keypress', function (e) {
                if (e.which == 32)
                    return false;
            });


            OnLoad();
            $(".Upload").click(function () {
                UploadImage();
                return false;
            });

            $(".btnSearchCustomer").click(function () {

                Search(false, false);
                return false;

            });

            $(".btn_Save").click(function () {
                Save();
                return false;
            });

            $(".btnUpdateCustomer").click(function () {
                var TicketMaster = $('.hf_TicketMasterId').val();
                var customer = $(".hfCustomerId").val();
                ChangeCustomer(TicketMaster, customer);
                return false;
            });

            $(".btn_UpdateToEmail").click(function () {
                UpdateToEmail();
                return false;
            });

            $(".btn_UpdateCCEmail").click(function () {
                UpdateCCEmail();
                return false;
            });

            //$(".btn_UpdateAssignee").click(function () {
            //    UpdateAssignee();
            //    return false;
            //});

            $('.dvTicketStatusPopup').click(function () {
                $('.DivShowDetail').html($(this).find(".WFStatusPanel").html());
                ShowPopupDiv();
                return false;
            });
        }

        function ImageDelete(id) {
            DeleteAtachment(id);
        }

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

    </script>

</asp:Content>

