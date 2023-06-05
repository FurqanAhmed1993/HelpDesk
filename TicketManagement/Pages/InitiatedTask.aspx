<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="InitiatedTask.aspx.cs" Inherits="Pages_InitiatedTask" %>

<%@ Register Src="~/Controls/Shared/WFStatus.ascx" TagPrefix="uc1" TagName="WFStatus" %>
<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>
<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="uc1" TagName="PagingAndSorting" %>




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


        <div class="row-fluid ">
            <div class="span12">
                <div class="primary-head">
                    <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                </div>
                <ul class="breadcrumb">
                    <li><a href="/Default.aspx" class="icon-home"></a><span class="divider "><i class="icon-angle-right"></i></span></li>
                    <li class="active">Task<span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/InitiatedTask.aspx">Initiated Tasks  </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input runat="server" type="hidden" id="ChatTypeId" class="ChatTypeId" value="0" />
                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Initiated Tasks</h3>
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
                                    <label class="control-label">Status</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlStatusSearch" CssClass="span12 ddlStatusSearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Priority</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlPrioritySearch" CssClass="span12 ddlPrioritySearch"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Ticket No </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicketNo" runat="server" placeHolder="Ticket No " CssClass="span12  txtTicketNo"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="control-group span2">
                                    <label class="control-label">Title </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTitle" runat="server" placeHolder="Title" CssClass="span12  txtTitle"></asp:TextBox>
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
                                        <tr>
                                            <th>Ticket No</th>
                                            <th>Initiator</th>
                                            <th>Assignee</th>
                                            <th>Title</th>
                                            <th>Start Date</th>
                                            <th>End Date</th>
                                            <th>Priority</th>
                                            <th>Created On</th>
                                            <th>Status</th>
                                            <th style="text-align: center;">Action
                                            </th>
                                        </tr>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <span>
                                                        <input type="hidden" runat="server" id="hfTicketMasterIdRpt" class="hfTicketMasterIdRpt"
                                                            value='<%# Eval("TicketMasterId") %>' />
                                                        <input type="hidden" runat="server" id="hfTaskMasterIdRpt" class="hfTaskMasterIdRpt"
                                                            value='<%# Eval("TaskMasterId") %>' />
                                                        <input type="hidden" runat="server" id="hfDepartmentIdRpt"
                                                            value='<%# Eval("DepartmentId") %>' />
                                                        <input type="hidden" runat="server" id="hfAssigneeId" class="hfAssigneeId"
                                                            value='<%# Eval("AssigneeId") %>' />
                                                        <input type="hidden" runat="server" id="hfInitiatorId" class="hfInitiatorId"
                                                            value='<%# Eval("InitiatorId") %>' />
                                                        <input type="hidden" runat="server" id="hfDepartment"
                                                            value='<%# Eval("Department") %>' />
                                                        <input type="hidden" runat="server" id="hfAssignee"
                                                            value='<%# Eval("Assignee") %>' />


                                                        <%# Eval("TicketNo") %>
                                                        <br />
                                                        <b>
                                                            <asp:LinkButton ID="lnkbtnTaskDetail" Text="Show Task Details" runat="server" ForeColor="Blue" CssClass="lnkbtnTaskDetail"></asp:LinkButton>
                                                        </b></td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Initiator") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("AssignTo") %>
                                                    </span>
                                                    <%-- <asp:Label ID="lblAssignee" runat="server" Text="Label"></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Convert.ToString(Eval("Title")).ToString().Length>=20?Convert.ToString(Eval("Title")).Substring(0,20)+"...":Convert.ToString(Eval("Title")) %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Convert.ToDateTime(Eval("StartDate")).ToString(Constant.DateFormat) %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Convert.ToDateTime(Eval("EndDate")).ToString(Constant.DateFormat) %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("Priority") %>
                                                    </span>
                                                </td>

                                                <td>

                                                    <strong><span style="color: blue">
                                                        <%# Convert.ToDateTime(Eval("TicketCreationDate")).ToString(Constant.DateFormat) %>
                                                    </span>
                                                        <br />
                                                        <span style="color: red">
                                                            <%# Convert.ToDateTime(Eval("TicketCreationDate").ToString()).ToString(Constant.TimeFormatAMPM) %>
                                                        </span>
                                                    </strong>
                                                </td>

                                                <td style="text-align: center;">
                                                    <%# Eval("Status") %>
                                                    <div id="dvTicketStatusPopup" class="dvTicketStatusPopup">
                                                        <uc1:WFStatus runat="server" ID="WFStatus" Type="Task"  MasterId='<%# Eval("TaskMasterId") %>' Visible="true" />
                                                    </div>
                                                </td>

                                                <td style="text-align: center;">
                                                    <div>
                                                        <asp:Label ID="lblNotification" runat="server" Text='<%#Eval("ChatNotificationCount")  %>' Visible='<%# (Convert.ToInt32(Eval("ChatNotificationCount")) > 0)?true:false%>' CssClass="notification_"></asp:Label>
                                                        <input title="Reply/Response" data-toggle="modal" data-target="#myModal" class="btnResponse" type="image" src="/images/mail_reply.png">
                                                    </div>
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

            $(".btnResponse").click(function () {
                var Url = "/pages/TicketReplyRespone.aspx?ChatTypeId=" + $('.ChatTypeId').val() + "&&TMID=" + $(this).closest("tr").find(".hfTaskMasterIdRpt").val() + "&&InitiatorId=" + $(this).closest("tr").find(".hfInitiatorId").val() + "&&AssigneeId=" + $(this).closest("tr").find(".hfAssigneeId").val() + "";
                IframePop(Url);
            });

            $(".lnkbtnTaskDetail").click(function () {
                var url = "/pages/TaskDetail.aspx?IsInitiator=1&&IsView=1&&TMID=" + $(this).closest("tr").find(".hfTicketMasterIdRpt").val() + "&&hfTaskMasterId=" + $(this).closest("tr").find(".hfTaskMasterIdRpt").val() + "";
                //$(location).attr('href', url);
                openPopupFancy(url);
            });

            $('.dvTicketStatusPopup').click(function () {
                $('.DivShowDetail').html($(this).find(".WFStatusPanel").html());
                ShowPopupDiv();
            });


            $(".btnClosePoP").click(function () {
                IframeClosePop();
                $('.btn_Search').click();
                return false;
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

