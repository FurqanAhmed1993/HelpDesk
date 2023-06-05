<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="MyMeetings.aspx.cs" Inherits="Pages_MyMeetings" %>

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
                   <%-- <li class="active">Meetings <span class="divider"><i class="icon-angle-right"></i></span></li>--%>
                    <li><a href="/Pages/MyMeetings.aspx">My Meetings  </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>My Meetings</h3>
                        </div>
                        <div class="widget-container">

                            <div class="controls-row">

                                <div class="control-group span2">
                                    <label class="control-label">Meeting Date</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtMeetingDate" runat="server" placeHolder="Meeting Date" CssClass="span12 datePicker txtMeetingDate"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Ticket No </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicketNo" runat="server" placeHolder="Ticket No " CssClass="span12  txtTicketNo"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="control-group span2">
                                    <label class="control-label">Meeting Agenda </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtMeetingAgenda" runat="server" placeHolder="Meeting Agenda " CssClass="span12  txtMeetingAgenda"></asp:TextBox>
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
                                        <th>Meeting Date  </th>
                                        <th>Attendee</th>
                                        <th>Meeting Agenda</th>
                                        <th>Start Time </th>
                                        <th>End Time</th>
                                       <%-- <th style="text-align: center;">Create Meeting </th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>

                                                    <span>
                                                        <input type="hidden" runat="server" id="hfAttendee" class="hfAttendee"
                                                            value='<%# Eval("Attendee") %>' />
                                                        <input type="hidden" runat="server" id="hf_TicketMasterId" class="hf_TicketMasterId"
                                                            value='<%# Eval("TicketMasterId") %>' />
                                                        <input type="hidden" runat="server" id="hfTicketMeetingId" class="hfTicketMeetingId"
                                                            value='<%# Eval("TicketMeetingId") %>' />
                                                        <%# Eval("TicketCode") %>
                                                        <br />
                                                        <b>
                                                            <asp:LinkButton ID="lnkbtnMeetingDetail" Text="Show Meeting Details" runat="server" ForeColor="Blue" CssClass="lnkbtnMeetingDetail"></asp:LinkButton>
                                                        </b>
                                                    </span>
                                                </td>
                                               

                                                <td>
                                                    <span>
                                                        <%# Convert.ToDateTime(Eval("MeetingDate")).ToString(Constant.DateFormat) %>
                                                    </span>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblAttendee" runat="server" Text=""></asp:Label>
                                                </td>


                                                <td>
                                                    <span>
                                                        <%# Convert.ToString(Eval("MeetingAgenda")).ToString().Length>=20?Convert.ToString(Eval("MeetingAgenda")).Substring(0,20)+"...":Convert.ToString(Eval("MeetingAgenda")) %>
                                                    </span>
                                                </td>

                                                <td>
                                                    <span>
                                                        <%# Eval("StartTime") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <%# Eval("EndTime") %>
                                                    </span>
                                                </td>

                                              <%--  <td style="text-align: center;">
                                                 
                                                    <asp:ImageButton ID="btnAddMeeting" CssClass="btnAddMeeting" runat="server" ToolTip="Create Meeting Request"
                                                        ImageUrl="~/images/ic_meeting.png" Height="24px" Width="24px" />
                                                </td>--%>

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


            $(".btnAddMeeting").click(function () {
                openPopupFancy("/pages/Meeting.aspx?IsView=0&&TMID=" + $(this).closest("tr").find(".hf_TicketMasterId").val() + "&&hfTicketMeetingId=0");
            });

            $(".lnkbtnMeetingDetail").click(function () {
                openPopupFancy("/pages/Meeting.aspx?IsView=1&&TMID=" + $(this).closest("tr").find(".hf_TicketMasterId").val() + "&&hfTicketMeetingId=" + $(this).closest("tr").find(".hfTicketMeetingId").val() + "");
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

