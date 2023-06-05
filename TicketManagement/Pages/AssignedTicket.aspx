<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="AssignedTicket.aspx.cs" Inherits="Pages_AssignedTicket" %>

<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>
<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="uc1" TagName="PagingAndSorting" %>
<%@ Register Src="~/Controls/Shared/WFStatus.ascx" TagPrefix="uc1" TagName="WFStatus" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid">

        <div>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <uc1:InProgress ID="InProgress2" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input runat="server" type="hidden" id="ChatTypeId" class="ChatTypeId" value="0" />
                <div class="row-fluid ">
                    <div class="span12">
                        <div class="primary-head">
                            <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                        </div>
                        <ul class="breadcrumb">
                            <li><a href="/Default.aspx" class="icon-home"></a><span class="divider "><i class="icon-angle-right"></i></span></li>
                            <li class="active">Ticket <span class="divider"><i class="icon-angle-right"></i></span></li>
                            <li><a href="/Pages/AssignedTicket.aspx">Assigned Tickets  </a></li>
                        </ul>
                    </div>
                </div>
                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Assigned Tickets</h3>
                        </div>
                        <div class="widget-container">
                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="control-label">Assignee</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAssigneeSearch" CssClass="span12 ddlAssigneeSearch"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="control-label">Customer Type</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlCustomerTypeSearch" CssClass="span12 ddlCustomerTypeSearch" OnSelectedIndexChanged="ddlCustomerTypeSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="control-label">Customer</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlCustomerSearch" CssClass="span12 ddlCustomerSearch" OnSelectedIndexChanged="ddlCustomerSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="control-label">Address</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlAddressSearch" CssClass="span12 ddlAddressSearch"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>



                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="control-label">Priority</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlPrioritySearch" CssClass="span12 ddlPrioritySearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="control-label">Ticket No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicket" runat="server" placeHolder="Ticket No" CssClass="span12  txtTicket"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="control-group span3">
                                    <label class="control-label">Primary IP</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPrimaryIP" runat="server" placeHolder="Primary IP" CssClass="span12  txtPrimaryIP"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span3">
                                    <label class="control-label">CAM No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCAMNo" runat="server" placeHolder="CAM No" CssClass="span12  txtCAMNo"></asp:TextBox>
                                    </div>
                                </div>






                            </div>



                            <div class="controls-row">


                                <div class="control-group span3">
                                    <label class="control-label">Title</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTitleSearch" runat="server" placeHolder="Title" CssClass="span12  txtTitleSearch"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="control-group span3">
                                    <label class="control-label">Un Assigned Ticket(s)</label>
                                    <div class="controls">
                                        <asp:CheckBox ID="chkbxUnAssignedTicket" runat="server" />
                                    </div>
                                </div>


                                <div class="control-group span6">
                                    <label class="control-label">Status</label>
                                    <div class="controls">
                                        <asp:CheckBoxList ID="chkbxlstStatus" runat="server" CssClass="cblCheckAll" CellPadding="10" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    </div>
                                </div>

                            </div>

                            <div class="controls-row">
                                <div class="pull-right">
                                    <asp:Button runat="server" ID="btn_Search" OnClick="btn_Search_Click" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                    <asp:Button runat="server" ID="btn_Cancel" OnClick="btn_Cancel_Click" Text="Cancel" class="btn btn-warning"></asp:Button>
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
                        </div>
                        <div class="widget-container">

                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                <thead>
                                    <tr>
                                        <th>Ticket No</th>
                                        <th>Initiator </th>
                                        <th>Assignee</th>
                                        <th>Product</th>
                                        <th>Customer Type</th>
                                        <th>Customer</th>
                                        <th>Address</th>
                                        <th>Problem Category </th>
                                        <th>CAM No </th>
                                        <th>Primary IP </th>
                                        <th>Title</th>
                                        <th>Created On</th>
                                        <th style="width: 50px; text-align: center;">Status</th>
                                        <th style="text-align: center;">Action</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr id="tr1" runat="server">
                                                <td>
                                                    <span>


                                                        <button type="button" class="myModal lnkbtnTicketDetail" data-toggle="modal" data-target="#myModal" style="color: blue; background: none; border: none; text-align: left; text-decoration: underline"><%# Eval("TicketCode") %></button>
                                                        <input type="hidden" runat="server" id="hfTicketMasterIdRpt" class="hfTicketMasterIdRpt"
                                                            value='<%# Eval("TicketMasterId") %>' />
                                                        <input type="hidden" runat="server" id="hfTicketDepartmentId"
                                                            value='<%# Eval("TicketDepartmentId") %>' />
                                                        <input type="hidden" runat="server" id="hfTicketStatusId"
                                                            value='<%# Eval("TicketStatusId") %>' />
                                                        <input type="hidden" runat="server" id="hfTicketAssigneeId" class="hfTicketAssigneeId"
                                                            value='<%# Eval("TicketAssigneeId") %>' />
                                                        <input type="hidden" runat="server" id="hfTicketInitiatorId" class="hfTicketInitiatorId"
                                                            value='<%# Eval("TicketInitiatorId") %>' />
                                                        <input type="hidden" runat="server" id="hfBackgroundColor"
                                                            value='<%# Eval("BackgroundColor") %>' />
                                                        <input type="hidden" runat="server" id="hfAccountId"
                                                            value='<%# Eval("AccountId") %>' />
                                                        <input type="hidden" runat="server" id="hflevelId"
                                                            value='<%# Eval("LevelId") %>' />
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
                                                <td>
                                                    <span>
                                                        <%# Eval("CustomerType") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Customer") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Address") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Convert.ToString(Eval("ServiceCategory")).ToString().Length>=20?Convert.ToString(Eval("ServiceCategory")).Substring(0,20)+"...":Convert.ToString(Eval("ServiceCategory")) %>
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
                                                    <span>
                                                        <%# Convert.ToString(Eval("TicketTitle")).ToString().Length>=20?Convert.ToString(Eval("TicketTitle")).Substring(0,20)+"...":Convert.ToString(Eval("TicketTitle")) %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <strong><span style="color: blue">
                                                        <%# Convert.ToDateTime(Eval("TicketCreationDate")).ToShortDateString() %>
                                                    </span>
                                                        <br />
                                                        <span style="color: red">
                                                            <%# Convert.ToDateTime("2015-01-04 " + Eval("TicketTime").ToString()).ToString(Constant.TimeFormatAMPM) %>
                                                        </span>
                                                    </strong>

                                                </td>
                                                <td style="text-align: center;">
                                                    <div>
                                                        <span>
                                                            <%# Eval("TicketStatus") %>
                                                        </span>
                                                    </div>

                                                    <br />
                                                    <div id="dvTicketStatusPopup" class="dvTicketStatusPopup ">
                                                        <uc1:WFStatus runat="server" ID="WFStatus" IsTicketStatus="true" Visible="true" MasterId=' <%# Eval("TicketMasterId") %>' />
                                                    </div>


                                                </td>

                                                <td id="Td1" style="text-align: center;" runat="server">

                                                    <input title="Create Meeting Request" class="btnAddMeeting" data-toggle="modal" data-target="#myModal" type="image" src="/images/ic_meeting.png">
                                                    <input title="Create Task Request" class="btnAddTask" data-toggle="modal" data-target="#myModal" type="image" src="/images/scheduled_tasks.png">

                                                    <div>
                                                        <asp:Label ID="lblNotification" runat="server" Text='<%#Eval("ChatNotificationCount")  %>' Visible='<%# (Convert.ToInt32(Eval("ChatNotificationCount"))!=0 &&Convert.ToString(Eval("TicketAssigneeId"))==UserId.ToString())?true:false%>' CssClass="notification_"></asp:Label>
                                                        <input title="Reply" data-toggle="modal" data-target="#myModal" class="btnReply" type="image" src="/images/mail_reply.png">
                                                    </div>

                                                    <input title="Interal Chat" class="btnAddComment" data-toggle="modal" data-target="#myModal" type="image" src="/images/comments-icon.png">
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
                <input type="image" src="/Images/close-button.png" class="btnClosePoP" />
            </div>
            <iframe src="" style="height: 100%; width: 100%;"></iframe>
        </div>
    </div>


    <script type="text/javascript">

        function pageLoad() {

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
            $(".lnkbtnTicketDetail").click(function () {
                var Url = "/pages/InitiateTicketsDetails.aspx?ControlFalse=False&&hf_IsInitiator=False&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
                IframePop(Url);
            });
            $(".btnReply").click(function () {
                var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.ChatTypeId').val() + "&&IsReply=True&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfTicketAssigneeId").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfTicketInitiatorId").val() + "";
                IframePop(Url);
            });
            $(".btnAddComment").click(function () {
                var Url = "/pages/TicketComment.aspx?TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "";
                IframePop(Url);
            });
            $(".btnAddTask").click(function () {
                var Url = "/pages/Task.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTaskMasterId=0";
                IframePop(Url);
            });
            $(".btnAddMeeting").click(function () {
                var Url = "/pages/Meeting.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTicketMeetingId=0";
                IframePop(Url);
            });
            $(".btnClosePoP").click(function () {
                IframeClosePop();
                $('.btn_Search').click();
                return false;
            });
            $('.dvTicketStatusPopup').click(function () {
                $('.DivShowDetail').html($(this).find(".WFStatusPanel").html());
                ShowPopupDiv();
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
            $('.btn_Search').trigger('click');
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
                openSpeed: 150,
                closeEffect: 'elastic',
                closeSpeed: 150,
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

    </script>
</asp:Content>

