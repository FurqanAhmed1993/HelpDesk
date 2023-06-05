<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="InitiatedTicket.aspx.cs" Inherits="Pages_InitiatedTicket" %>

<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>
<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="uc1" TagName="PagingAndSorting" %>
<%@ Register Src="~/Controls/Shared/WFStatus.ascx" TagPrefix="uc1" TagName="WFStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">

        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <uc1:InProgress ID="InProgress2" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div class="row-fluid ">
            <div class="span12">
                <div class="primary-head">
                    <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                </div>
                <ul class="breadcrumb">
                    <li><a href="/Default.aspx" class="icon-home"></a><span class="divider "><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/InitiatedTicket.aspx">Tickets  </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <input runat="server" type="hidden" id="Ticket_Reply_Response" class="Ticket_Reply_Response" value="0" />
                <input runat="server" type="hidden" id="Ticket_Internel_Chat" class="Ticket_Internel_Chat" value="0" />
                <div class="row-fluid">
                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Tickets </h3>
                            <a href="/Pages/CreateTicket.aspx" runat="server" id="btnCreateTicket" class="btnCreateTicket btn btn-info pull-right" style="margin-top: -35px; margin-right: 10px;">Initiate Ticket </a>
                        </div>
                        <div class="widget-container">

                            <div class="controls-row">

                                <div class="control-group span2">
                                    <label class="control-label">Initiator</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlInitiatorSearch" CssClass="span12 ddlInitiatorSearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Department (Assiged)</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlDepartmentSearch" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentSearch_SelectedIndexChanged" CssClass="span12 ddlDepartmentSearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Assignee</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAssigneeSearch" CssClass="span12 ddlAssigneeSearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Un Assigned Ticket(s)</label>
                                    <div class="controls">
                                        <asp:CheckBox ID="chkbxUnAssignedTicket" runat="server" CssClass="CheckBox_css" />
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Priority</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlPrioritySearch" CssClass="span12 ddlPrioritySearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Ticket No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicket" runat="server" placeHolder="Ticket No" CssClass="span12  txtTicket"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="controls-row">
                                <div class="control-group span2">
                                    <label class="control-label">Customer Type</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlCustomerTypeSearch" CssClass="span12 ddlCustomerTypeSearch" OnSelectedIndexChanged="ddlCustomerTypeSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Customer</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlCustomerSearch" CssClass="span12 ddlCustomerSearch" OnSelectedIndexChanged="ddlCustomerSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Address</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAddressSearch" CssClass="span12 ddlAddressSearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Primary IP</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPrimaryIP" runat="server" placeHolder="Primary IP" CssClass="span12  txtPrimaryIP"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">CAM No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCAMNo" runat="server" placeHolder="CAM No" CssClass="span12  txtCAMNo"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Title</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTitleSearch" runat="server" placeHolder="Title" CssClass="span12  txtTitleSearch"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="controls-row">
                                <div class="control-group span6">
                                    <label class="control-label">Status</label>
                                    <div class="controls">
                                        <asp:CheckBoxList ID="chkbxlstStatus" runat="server" CssClass="cblCheckAll" CellPadding="10" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    </div>
                                </div>

                                <div class="control-group span6">
                                    <div class="pull-right" style="margin-top: 35px;">
                                        <asp:Button runat="server" ID="btn_Search" OnClick="btn_Search_Click" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                        <asp:Button runat="server" ID="btn_Cancel" OnClick="btn_Cancel_Click" Text="Cancel" class="btn btn-warning"></asp:Button>
                                        <%-- <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" Text="Send Test Email" class="btn btn-warning"></asp:Button>--%>
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
                            <h3>Records</h3>
                            <button type="button" class="HistoryModal btn btn-info pull-right btn_HistoryModal" data-toggle="modal" data-target="#HistoryModal" style="margin-top: -35px; margin-right: 10px; display: none"></button>
                        </div>
                        <div class="widget-container">
                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                <thead>
                                    <tr>
                                        <th>Ticket No</th>
                                        <th>Initiator </th>
                                        <th>Assignee</th>
                                        <th>Product</th>
                                        <th>Customer</th>
                                        <th>Address</th>
                                        <th>Title</th>
                                        <th>Problem Category  </th>
                                        <th>CAM No </th>
                                        <th>Primary IP </th>
                                        <th>Created On</th>
                                        <th style="width: 50px; text-align: center;">Link History</th>
                                        <th style="width: 50px; text-align: center;">Status</th>
                                        <th style="width: 50px; text-align: center;">RFO</th>
                                        <th style="text-align: center;">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr id="tr1" runat="server" class="tr1">
                                                <td class="td_LinkButton">
                                                    <span>
                                                        <%--<button type="button" class="myModal lnkbtnTicketDetail" data-toggle="modal" data-target="#myModal" style="color: blue; background: none; border: none; text-align: left; text-decoration: underline"><%# Eval("TicketCode") %></button>--%>
                                                        <button type="button" class="lnkbtnTicketDetail" style="color: blue; background: none; border: none; text-align: left; text-decoration: underline"><%# Eval("TicketCode") %></button>
                                                    </span>

                                                    <br />
                                                    &nbsp  <strong>Region : </strong><%# Eval("Region") %>
                                                    <br />
                                                    &nbsp  <strong>City : </strong><%# Eval("City") %>
                                                    <br />
                                                    &nbsp  <strong>Priority : </strong><%# Eval("TicketPriority") %>
                                                    <br />
                                                    &nbsp  <strong>Request Type : </strong><%# Eval("RequestType") %>


                                                    <input type="hidden" runat="server" id="hfTicketMasterIdRpt" class="hfTicketMasterIdRpt"
                                                        value='<%# Eval("TicketMasterId") %>' />
                                                    <input type="hidden" runat="server" id="hfTicketDepartmentId"
                                                        value='<%# Eval("TicketDepartmentId") %>' />
                                                    <input type="hidden" runat="server" id="hfTicketAssigneeId" class="hfTicketAssigneeId"
                                                        value='<%# Eval("TicketAssigneeId") %>' />
                                                    <input type="hidden" runat="server" id="hfTicketInitiatorId" class="hfTicketInitiatorId"
                                                        value='<%# Eval("InitiatorId") %>' />
                                                    <input type="hidden" runat="server" id="hfTicketStatusId" class="hfTicketStatusId"
                                                        value='<%# Eval("TicketStatusId") %>' />
                                                    <input type="hidden" runat="server" id="hfBackgroundColor"
                                                        value='<%# Eval("BackgroundColor") %>' />
                                                    <input type="hidden" runat="server" id="Assignee" class="Assignee"
                                                        value='<%# Eval("Assignee") %>' />
                                                    <input type="hidden" runat="server" id="hfTicketdDepartment" class="hfTicketdDepartment"
                                                        value='<%# Eval("Department") %>' />
                                                    <input type="hidden" runat="server" id="hfProductId" class="hfProductId"
                                                        value='<%# Eval("ProductId") %>' />
                                                    <input type="hidden" runat="server" id="hfManageSevicesMasterId" class="hfManageSevicesMasterId"
                                                        value='<%# Eval("ManageSevicesMasterId") %>' />
                                                    <input type="hidden" runat="server" id="hfDetailid" class="hfDetailid"
                                                        value='<%# Eval("Detailid") %>' />
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("TicketInitiatorName") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("AssignTo") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Product") %>
                                                    </span>
                                                </td>

                                                <%--    <td>
                                                    <span>
                                                        <%# Eval("CustomerType") %>
                                                    </span>
                                                </td>--%>
                                                <td>
                                                    <span>
                                                        <strong><%# Eval("CustomerType") %></strong>
                                                    </span>
                                                    <br />
                                                    <span>
                                                        <%# Eval("Customer") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Address") %>
                                                    </span>
                                                </td>
                                                <td title='<%# Eval("TicketTitle") %>'>
                                                    <span>
                                                        <label>
                                                            <%# Convert.ToString(Eval("TicketTitle")).ToString().Length>=20?Convert.ToString(Eval("TicketTitle")).Substring(0,20)+"...":Convert.ToString(Eval("TicketTitle")) %>
                                                        </label>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("ServiceCategory") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("CAMNo") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("PrimaryIP") %>
                                                    </span>
                                                </td>

                                                <td>
                                                    <strong>
                                                        <span class="Date_span" style="color: blue">
                                                            <%# Convert.ToDateTime(Eval("TicketCreationDate")).ToShortDateString() %>
                                                        </span>
                                                        <br />
                                                        <span class="Time_span" style="color: red">
                                                            <%# Convert.ToDateTime("2015-01-04 " + Eval("TicketCreationTime").ToString()).ToString(Constant.TimeFormatAMPM) %>
                                                        </span>
                                                    </strong>
                                                </td>
                                                <td style="text-align: center;">
                                                    <br />
                                                    <asp:ImageButton runat="server" ID="btn_TicketHistory" Visible='<%# Convert.ToInt32(Eval("LinkCount")) > 0 ? true : false %>' OnClick="btn_TicketHistory_Click" CssClass="icon" Width="40" Height="40" ImageUrl="../Images/HistoryIcon.png" />
                                                </td>
                                                <td id="dvTicketStatusPopup" class="dvTicketStatusPopup Td_Status" style="text-align: center;" title='<%# Eval("TicketStatus") %>'>
                                                    <div>
                                                        <span>
                                                            <%# Eval("TicketStatus") %>
                                                        </span>
                                                    </div>
                                                    <br />
                                                    <uc1:WFStatus runat="server" ID="WFStatus" Type="Ticket" Status='<%# Eval("TicketStatus") %>' Visible="true" MasterId=' <%# Eval("TicketMasterId") %>' />

                                                </td>

                                                <td style="text-align: center;">
                                                    <br />
                                                    <asp:ImageButton ToolTip="Generate Reason For Outage" runat="server" ID="btnRFO" Visible='<%# base.CustomerId > 0 ? false : (Convert.ToInt32(Eval("TicketStatusId")) == (int)Constant.TicketStatus.Resolved ? (Convert.ToInt32(Eval("RFOCount")) > 0 ? false : true)  : false) %>' CssClass="icon btnRFO" Width="40" Height="40" ImageUrl="../Images/pdf.png" />
                                                </td>

                                                <td style="text-align: center;">
                                                    <div class="tdReply_Responce">
                                                        <asp:Label ID="lblNotification" runat="server" Text='<%# Eval("ChatNotificationCount") %>' Visible='<%# Convert.ToInt32(Eval("ChatNotificationCount")) > 0 ?true : false %>' CssClass="notification_"></asp:Label>
                                                        <input title="Reply/Response" class="btnResponse" type="image" src="/images/mail_reply.png">
                                                    </div>
                                                    <input title="Create Meeting Request" class="btnAddMeeting" style="visibility: <%# Eval("ClientVisiblity") %>" type="image" src="/images/ic_meeting.png">
                                                    <input title="Create Task Request" class="btnAddTask" style="visibility: <%# Eval("ClientVisiblity") %>" type="image" src="/images/scheduled_tasks.png">

                                                    <div>
                                                        <asp:Label ID="lblInterchatCount" runat="server" Text='<%# Eval("InternalChatNotificationCount") %>' Visible='<%#  IsClient == true ? false : Convert.ToInt32(Eval("InternalChatNotificationCount")) > 0 ?true : false %>' CssClass="notification_"></asp:Label>
                                                        <input title="Internal Chat" class="btnAddComment" style="visibility: <%# Eval("ClientVisiblity") %>" type="image" src="/images/comments-icon.png">
                                                    </div>
                                                    <asp:ImageButton ToolTip="Edit" runat="server" ID="btn_Edit" Visible='<%# base.CustomerId > 0 ? false : (Convert.ToInt32(Eval("TicketStatusId")) != (int)Constant.TicketStatus.New && Convert.ToInt32(Eval("TicketStatusId")) != (int)Constant.TicketStatus.InProgress ? false : 
                                                    (Convert.ToInt32(Eval("TicketStatusId")) == (int)Constant.TicketStatus.New && base.DepartmentId == Convert.ToInt32(Eval("TicketDepartmentId")) ? true   : (Convert.ToInt32(Eval("TicketStatusId")) == (int)Constant.TicketStatus.InProgress && base.UserId == Convert.ToInt32(Eval("InitiatorId")) ? true : false || IsAdmin || IsSuperAdmin))) %>'
                                                        CssClass="btn_Edit" ImageUrl="/images/edit-icon-Pencil.png" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                    <div class="pagging">
                                        <uc1:PagingAndSorting runat="server" ID="PagingAndSorting" />
                                    </div>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <div class='manageCategory DivShowDetail'>
    </div>
    <div id="myModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="left: 5%; width: 90%; height: 90%; margin-left: 0px; top: 3%; background-color: transparent">
        <div class="modal-dialog" style="height: 100%;">
            <div class="pull-right" style="margin-right: -10px">
                <input id="btnClosePoP" type="image" src="/Images/close-button.png" class="btnClosePoP" />
            </div>
            <iframe src="" style="height: 100%; width: 100%;"></iframe>
        </div>
    </div>

    <div id="HistoryModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="width: 700px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row-fluid">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="content-widgets light-gray">
                                    <div class="widget-head bluePSW">
                                        <h3>Link History</h3>
                                    </div>
                                    <div class="widget-container">

                                        <div class="controls-row">
                                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Ticket No</th>
                                                        <th>Last Finding</th>
                                                        <th>Ticket Date</th>
                                                        <th style="text-align: center;">Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpt_History" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 14%; color: blue"><%# Eval("TicketCode") %></td>
                                                                <td><%# Eval("Description") %></td>
                                                                <td style="text-align: center; width: 14%;">
                                                                    <%# Convert.ToDateTime(Eval("CreatedDate").ToString()).ToString(Constant.DateFormat) %>
                                                                </td>
                                                                <td style="text-align: center; width: 14%;">
                                                                    <%# Eval("Status") %>
                                                                    <br />
                                                                    <img src='<%# Eval("imageUrl") %>' class="icons" style="height: 30px;" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="controls-row">
                                            <div class="pull-right">
                                                <asp:Button runat="server" ID="btn_Close" Text="Close" OnClick="btn_Close_Click" class="btn btn-warning"></asp:Button>
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

    <script type="text/javascript">

        function pageLoad() {

            $('.tr1').each(function () {
                var StatusId = $(this).find(".hfTicketStatusId").val();
                var ManageSevicesMasterId = $(this).find(".hfManageSevicesMasterId").val();
                if (StatusId == 8) {
                    $(this).find('.tdReply_Responce').hide();
                    $(this).find('.btnAddMeeting').hide();
                    $(this).find('.btnAddTask').hide();
                    $(this).find('td').css("background-color", "#e88a05");
                    $(this).find('td').css("color", "white");
                    $(this).find('.lnkbtnTicketDetail').css("color", "white");
                    $(this).find('.Date_span').css("color", "white");
                    $(this).find('.Time_span').css("color", "white");
                }
                else if (ManageSevicesMasterId == 0) {
                    $(this).find('.tdReply_Responce').hide();
                    $(this).find('.btnAddMeeting').hide();
                    $(this).find('.btnAddTask').hide();
                }

            });

            $(".btnRFO").click(function () {
                var url = "/pages/CreateRFO.aspx?TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
                $(location).attr('href', url);
                return false;
            });

            $(".btn_HistoryModal").click(function () {
                $('#HistoryModal').zIndex(1050);
            });

            $(".btn_Edit").click(function () {
                var url = "/pages/CreateTicket.aspx?TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
                $(location).attr('href', url);
            });


            $(".td_LinkButton").click(function () {
                var Url = "/pages/InitiateTicketsDetails.aspx?ControlFalse=False&&hf_IsInitiator=True&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
                openPopupFancy(Url);
            });

            //$(".lnkbtnTicketDetail").click(function () {
            //    var Url = "/pages/InitiateTicketsDetails.aspx?ControlFalse=False&&hf_IsInitiator=True&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
            //    //IframePop(Url);
            //    openPopupFancy(Url);
            //    //$(location).attr('href', Url);
            //});
            $(".btnResponse").click(function () {
                var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.Ticket_Reply_Response').val() + "&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfTicketInitiatorId").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfTicketAssigneeId").val() + "";
                //IframePop(Url);
                openPopupFancy(Url);
                  //$(location).attr('href', Url);
            });
            $(".btnAddTask").click(function () {
                var Url = "/pages/Task.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTaskMasterId=0";
                //IframePop(Url);
                openPopupFancy(Url);
            });
            $(".btnAddMeeting").click(function () {
                var Url = "/pages/Meeting.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTicketMeetingId=0";
                //IframePop(Url);
                openPopupFancy(Url);
            });
            $(".btnAddComment").click(function () {
                var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.Ticket_Internel_Chat').val() + "&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfTicketInitiatorId").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfTicketAssigneeId").val() + "";
                //IframePop(Url);
                openPopupFancy(Url);
                //$(location).attr('href', Url);
            });
            $(".btnClosePoP").click(function () {
                IframeClosePop();
                $('.btn_Search').click();
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
            ShowTicketStatusPopup();
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


        function ShowTicketStatusPopup() {
            $('.dvTicketStatusPopup').click(function () {
                $('.DivShowDetail').html($(this).find(".WFStatusPanel").html());
                ShowPopupDiv();
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
        function CloseFancyBox() {
            $.fancybox.close();
        }


        function ClosePopup() {
            $('#HistoryModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function OpenPopup() {
            $('.btn_HistoryModal').click();
            $('#HistoryModal').zIndex(1050);
        }

    </script>

</asp:Content>

