<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Tickets.aspx.cs" Inherits="Pages_Tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/Tickets.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
    <script src="/js/crypto-js.js"></script>
    <script src="/js/Pages_JS/Cryptography.js"></script>
    <style>
        .blink_red {
            -webkit-animation: mymove_red 2s infinite;
            animation: mymove_red 2s infinite;
        }

        @keyframes mymove_red {
            50% {
                background: red;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <input runat="server" type="hidden" id="Ticket_Reply_Response" class="Ticket_Reply_Response" value="0" />
        <input runat="server" type="hidden" id="Ticket_Internel_Chat" class="Ticket_Internel_Chat" value="0" />
        <input runat="server" type="hidden" id="hfRoleId" class="hfRoleId" value="0" />
        <input runat="server" type="hidden" id="hfDepartmentId" class="hfDepartmentId" value="0" />
        <input runat="server" type="hidden" id="hfCustomerId" class="hfCustomerId" value="0" />
        <input runat="server" type="hidden" id="hfCustomerTypeId" class="hfCustomerTypeId" value="0" />
        <input runat="server" type="hidden" id="hfUserId" class="hfUserId" value="0" />
        <input runat="server" type="hidden" id="HfIsAdmin" class="HfIsAdmin" value="0" />
        <input runat="server" type="hidden" id="HfIsSuperAdmin" class="HfIsSuperAdmin" value="0" />

        <div class="row-fluid ">
            <div class="span12">
                <div class="primary-head">
                    <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                </div>
                <%--   <ul class="breadcrumb">
                    <li><a href="/Default.aspx" class="icon-home"></a><span class="divider "><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/Tickets.aspx">Tickets  </a></li>
                </ul>--%>
            </div>
        </div>


        <div class="row-fluid">
            <div class="content-widgets light-gray">
                <div class="widget-head bluePSW">
                    <h3>Tickets</h3>
                    <a href="/Pages/CreateTicket.aspx" runat="server" id="btnCreateTicket" class="btnCreateTicket btn btn-info pull-right" style="margin-top: -35px; margin-right: 10px;">Initiate Ticket </a>
                </div>
                <div class="widget-container">

                    <div class="controls-row">
                        <div class="control-group span2">
                            <label class="control-label">Ticket Created Date From</label>
                            <div class="controls">
                                <asp:TextBox ID="txtTicketDateFrom" runat="server" placeHolder="Ticket Created Date From" CssClass="span12 datePicker txtTicketDateFrom"></asp:TextBox>
                            </div>
                        </div>

                        <div class="control-group span2">
                            <label class="control-label">Ticket Created Date To</label>
                            <div class="controls">
                                <asp:TextBox ID="txtTicketDateTo" runat="server" placeHolder="Ticket Created Date To" CssClass="span12 datePicker txtTicketDateTo"></asp:TextBox>
                            </div>
                        </div>

                        <div class="control-group span2">
                            <label class="control-label">Initiator</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="ddlInitiatorSearch" CssClass="span12 ddlInitiatorSearch"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="control-group span2">
                            <label class="control-label">Department</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="ddlDepartmentSearch" CssClass="span12 ddlDepartmentSearch"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="control-group span2">
                            <label class="control-label">Assignee</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="ddlAssigneeSearch" CssClass="span12 ddlAssigneeSearch"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="control-group span2">
                            <label class="control-label">Priority</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="ddlPrioritySearch" CssClass="span12 ddlPrioritySearch"></asp:DropDownList>
                            </div>
                        </div>

                    </div>



                    <div class="controls-row">


                        <div class="control-group span2">
                            <label class="control-label">Ticket No</label>
                            <div class="controls">
                                <asp:TextBox ID="txtTicket" runat="server" placeHolder="Ticket No" CssClass="span12  txtTicket"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group span2">
                            <label class="control-label">Customer Name</label>
                            <div class="controls">
                                <asp:TextBox ID="txtCustomerName" runat="server" placeHolder="Customer Name" CssClass="span12  txtCustomerName"></asp:TextBox>
                            </div>
                        </div>

                        <div class="control-group span2">
                            <label class="control-label">Title</label>
                            <div class="controls">
                                <asp:TextBox ID="txtTitleSearch" runat="server" placeHolder="Title" CssClass="span12  txtTitleSearch"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group span2">
                            <label class="control-label">Customer Contact No</label>
                            <div class="controls">
                                <asp:TextBox ID="txtContactNo" runat="server" placeHolder="Customer Contact No" CssClass="span12  txtContactNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group span2">
                            <label class="control-label">Customer Alternative Contact No</label>
                            <div class="controls">
                                <asp:TextBox ID="txtAltContactNo" runat="server" placeHolder="Customer Alt Contact No" CssClass="span12  txtAltContactNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group span2">
                            <label class="control-label">Customer Email</label>
                            <div class="controls">
                                <asp:TextBox ID="txtEmail" runat="server" placeHolder="Customer Email" CssClass="span12  txtEmail"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="controls-row">
                         <div class="control-group span2">
                            <label class="control-label">Customer City</label>
                            <div class="controls">
                                 <asp:DropDownList runat="server" ID="ddlCity" CssClass="span12 ddlCity"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group span2">
                            <label class="control-label">Reported By</label>
                            <div class="controls">
                               <%-- <asp:TextBox ID="txtReportedBy" runat="server" placeHolder="Reported By" CssClass="span12 txtReportedBy"></asp:TextBox>--%>
                                 <asp:DropDownList runat="server" ID="ddlReportedBy" CssClass="span12 ddlReportedBy"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="controls-row">
                        <div class="control-group span6">
                            <label class="control-label">Status</label>
                            <div id="dvCheckBoxListControl" class="dvCheckBoxListControl">
                            </div>
                        </div>

                        <div class="control-group span6">
                            <div class="pull-right" style="margin-top: 15px;">
                                <asp:Button runat="server" ID="btn_Search" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" class="btn btn-warning btn_Cancel"></asp:Button>
                            </div>
                        </div>

                    </div>
                    <div class="controls-row btn-danger" id="divError_" runat="server" visible="false">
                        <div id="lblError_" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row-fluid">
            <div class="content-widgets light-gray">
                <div class="widget-head bluePSW">
                    <h3>Total Records :
                        <asp:Label runat="server" ID="TotalRecords" CssClass="TotalRecords" Text="0"></asp:Label></h3>
                </div>
                <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                    <thead>
                        <tr>
                            <th>Ticket No</th>
                            <th>Customer</th>
                            <th style="text-align: center;">Status</th>
                            <th>Ticket Creation Date & Time</th>
                            <th>Title</th>
                            <th>Contact Method</th>
                            <th>Request Type</th>
                            <th>Reported By</th>
                            <th>City</th>
                            <%--<th>Initiator </th>--%>
                            <th>Category</th>
                            <th>Subcategory</th>
                            <th>Assign To</th>
                            <th>Assignee</th>
                            <%--<th>Product</th>
                            <th>Product Sub-Category</th>--%>
                            <th>Closure Date & Time</th>
                            <th>Closure Findings</th>
                            <th>Total Resolution Time</th>
                            <th>Incident Description</th>
                            <th style="text-align: center;">Action</th>
                        </tr>
                    </thead>
                    <tbody class="wfForm">
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="wfForm">
        <tr class="tr">
            <td class="lnkbtnTicketDetail">
                <label style="color: #1f2e4c; font-weight: bold; background: none; border: none; text-align: left; text-decoration: underline" class="Bold lnkTicketDetail">${TicketCode}</label>
                <div class="cls_reopen" style="display: none;">
                    <strong>Re-Open Date : </strong>${Reopen_Date}
                </div>

                <%--  <br />
                <strong>Request Type : </strong>
                <br />
                <strong>Created On : </strong>--%>

                <input class="hfReopen_Date" type="hidden" value='${Reopen_Date}' />
                <input class="hfTicketMasterIdRpt" type="hidden" value='${TicketMasterId}' />
                <input class="hfTicketDepartmentId" type="hidden" value='${TicketDepartmentId}' />
                <input class="hfTicketAssigneeId" type="hidden" value='${TicketAssigneeId}' />
                <input class="hfTicketInitiatorId" type="hidden" value='${InitiatorId}' />
                <input class="hfTicketStatusId" type="hidden" value='${TicketStatusId}' />
                <input class="hfBackgroundColor" type="hidden" value='${BackgroundColor}' />
                <input class="Assignee" type="hidden" value='${Assignee}' />
                <input class="hfTicketdDepartment" type="hidden" value='${Department}' />
                <input class="hfLinkCount" type="hidden" value='${LinkCount}' />
                <input class="hfChatNotificationCount" type="hidden" value='${ChatNotificationCount}' />
                <input class="hfClientVisiblity" type="hidden" value='${ClientVisiblity}' />
                <input class="hfInternalChatNotificationCount" type="hidden" value='${InternalChatNotificationCount}' />
                <input class="hfReplyOtherUser" type="hidden" value='${Is_ReplyOtherUser}' />
                <input class="hfReplyFromL2" type="hidden" value='${Is_ReplyFromL2}' />
            </td>
            <td>${CustomerName} </td>
            <td class="Td_Status" style="text-align: center">${TicketStatus}
                
                <br />
                <br />
                <img src="${TicketStatusimageUrl}" />
            </td>
            <td>${TicketCreationDate1}</td>
            <td>${TicketTitle}</td>
            <td>${TicketRequestMode}</td>
            <td>${RequestType}</td>
            <td>${ReportedBy}</td>
            <td>${City}</td>
            <%--<td>${TicketInitiatorName}</td>--%>
            <td>${Category}</td>
            <td>${Subcategory}</td>
            <td>${AssignTo}</td>
            <td>${Assignee}</td>
            <%-- <td>${TypeOfIssue}</td>
            <td>${ProductSubCategory}</td>--%>
            <td>${Ticket_CloseDateTime}</td>
            <td>${TypeOfComplaint}</td>
            <td>${TotalResolutionTime}</td>
            <%--<td>${IncidentDescription_Closed}</td>--%>
            <td>{{html IncidentDescription_Closed}}</td>
            <td style="text-align: center;">
                <div class="tdReply_Responce">
                    <label class="notification_ lblNotification">${ChatNotificationCount}</label>
                    <asp:ImageButton ToolTip="Reply/Response" runat="server" ID="btnResponse" CssClass="icon btnResponse" Width="25" Height="25" ImageUrl="../Images/mail_reply.png" />
                </div>
                <div class="DivInterchatCount">
                    <label class="notification_ lblInterchatCount">${InternalChatNotificationCount}</label>
                    <asp:ImageButton ToolTip="Internal Chat" runat="server" ID="btnAddComment" CssClass="icon btnAddComment" Width="25" Height="25" ImageUrl="../Images/comments-icon.png" />
                </div>
                <div>
                    <asp:ImageButton ToolTip="Edit" runat="server" ID="btn_Edit" CssClass="btn_Edit" ImageUrl="/images/edit-icon-Pencil.png" />
                </div>
                <div>
                    <asp:ImageButton ToolTip="Delete Ticket" runat="server" ID="btn_Delete" CssClass="btn_Delete" Width="25" Height="25" ImageUrl="/images/Delete.png" />
                </div>
            </td>
        </tr>
    </script>

    <div id="div2" class="DivHideTicketStatusHistory DivShowTicketStatusHistory">
        <div style="overflow-y: scroll; max-height: 300px;">
            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered">
                <thead>
                    <tr>
                        <th>Step</th>
                        <th>Action by</th>
                        <th>Description</th>
                        <th>Product</th>
                        <th>Product Sub-Category</th>
                        <th>Findings</th>
                        <th style="text-align: center;">Date</th>
                        <th style="text-align: center;">Status</th>
                    </tr>
                </thead>
                <tbody class="wfTicketHistory">
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="wfTicketHistory">
        <tr class="tr">
            <td style="width: 5%;">${SNo}</td>
            <td style="width: 15%;">${CreatedBy}</td>
            <td style="width: 40%; white-space: pre-line;">{{html Description}}</td>
            <td style="width: 10%;">${TypeOfIssue}</td>
            <td style="width: 10%;">${ProductSubCategory}</td>
            <td style="width: 10%;">${TypeOfComplaint}</td>
            <td style="text-align: center; width: 10%;">${CreatedDate_}
            </td>
            <td style="text-align: center; width: 10%;">${Status}
                <br />
                <img src='${imageUrl}' class="icons" style="height: 30px;" />
            </td>
        </tr>
    </script>


    <div id="div1" class="DivHideTicketHistory DivShowTicketHistory">
        <div style="overflow-y: scroll; max-height: 300px;">
            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered">
                <thead>
                    <tr>
                        <th>Action By</th>
                        <th>Last Finding</th>
                        <th style="text-align: center;">Date</th>
                        <th style="text-align: center;">Status</th>
                    </tr>
                </thead>
                <tbody class="wfLinkHistory">
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="wfLinkHistory">
        <tr class="tr">
            <td>${ActionBy}</td>
            <td style="white-space: pre-line;">
                <label style="color: #8C0A0A; font-weight: bold;">${TicketCode}</label>
                <br />
                {{html Description}}
            </td>
            <td style="text-align: center;">${CreatedDate_}
            </td>

            <td style="text-align: center;">${Status}
                <br />
                <img src='${imageUrl}' class="icons" style="height: 30px;" />
            </td>
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

            $(".btn_Search").click(function () {
                Search();
                return false;
            });

            $(".btn_Cancel").click(function () {
                Cancel();
                return false;
            });

            $('.cblCheckAll input').change(function () {
                var currChk = $(this);
                if ($(this).val() == "0") {
                    $(this).closest('table').find('input:checkbox').prop('checked', $(currChk).is(':checked'));
                }
                else {
                    var allCheckboxCount = $(this).closest('table').find('input:checkbox').size();
                    var allCheckedCount = $(this).closest('table').find('input:checkbox:checked').not('input:checkbox[value=0]').size();
                    var isChecked = false;
                    if (allCheckedCount >= allCheckboxCount - 1) {
                        isChecked = true;
                    }
                    $(this).closest('table').find('input:checkbox[value=0]').prop('checked', isChecked);
                }
            });



        }
        //Search();
        //window.setInterval(function () {
        //    Search();
        //}, 10000);

    </script>

</asp:Content>

