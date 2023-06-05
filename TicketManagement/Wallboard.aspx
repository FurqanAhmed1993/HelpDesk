<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Wallboard.aspx.cs" Inherits="Wallboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/Wallboard.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .ForeColor {
            color: white;
            font-weight: bold;
        }



        .lblCount { 
            font-size: 50px;
            font-weight: bold;
        }

        .Bold {
            font-weight: bold;
        }

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



    <input type="hidden" id="Wallboard_Refresh_TimeInSec" class="Wallboard_Refresh_TimeInSec" runat="server" value="3" />
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="board-widgets gray">
                <div class="widget-head bluePSW">
                    <h3>Search</h3>
                </div>
                <div class="widget-container">
                    <div class="row-fluid ">
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
                        <div class="control-group span5">
                            <div style="margin-top: 23px;">
                                <asp:Button runat="server" ID="btn_Search" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" class="btn btn-warning btn_Cancel"></asp:Button>
                            </div>
                        </div>
                        <div class="control-group span3" style="text-align: right;">

                            <h3>
                                <b style="margin-right: 10px; font-size: large; color: #1f2e4c;">Last Refreshed On :
                                   
                                        <asp:Label runat="server" ID="lblDate" CssClass="lblDate"></asp:Label>
                                </b>
                            </h3>
                        </div>

                    </div>
                </div>
            </div>

        </div>



        <div class="row-fluid ">
            <div class="span12">
                <div class="board-widgets gray">
                    <%--    <div class="board-widgets-head clearfix">
                    </div>--%>
                    <div class="row-fluid ">
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('8');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblJunk" CssClass="lblJunk lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Status/Icons/New.png" /></span>
                                    <span class="widget-label ForeColor">New</span></a>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('1');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblNew" CssClass="lblNew lblCount" Text="0"></asp:Label>
                                    </span>
                                    <span class="widget-icon">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Status/Icons/Working.png" />
                                    </span>
                                    <span class="widget-label ForeColor">In-Progress</span>
                                </a>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('4');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblInProgress" CssClass="lblInProgress lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Status/Icons/Resolved.png" /></span>
                                    <span class="widget-label ForeColor">Escalated</span></a>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('6');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblOnHold" CssClass="lblOnHold lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Status/Icons/Onhold.png" /></span>
                                    <span class="widget-label ForeColor">On-Hold</span></a>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid ">
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('3');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblClosed" CssClass="lblClosed lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Status/Icons/Closed.png" /></span>
                                    <span class="widget-label ForeColor">Closed</span></a>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('7');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblReopen" CssClass="lblReopen lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Status/Icons/Reopen.png" /></span>
                                    <span class="widget-label ForeColor">Re-Open</span></a>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                <a target="_blank" href="javascript:void(0);" onclick="javascript:Redirect('2');">
                                    <span class="widget-stat">
                                        <asp:Label runat="server" ID="lblCancel" CssClass="lblCancel lblCount" Text="0"></asp:Label>
                                    </span><span class="widget-icon">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Status/Icons/Cancel.png" /></span>
                                    <span class="widget-label ForeColor">Cancel</span></a>
                            </div>
                        </div>

                        <%--<div class="span3">
                                <div class="board-widgets bondi-blue small-widget BoxBackgroundColor">
                                    <a target="_blank" href="/Pages/Tickets.aspx?StatusId=5">
                                        <span class="widget-stat">
                                            <asp:Label runat="server" ID="lblResolved" CssClass="lblResolved lblCount" Text="0"></asp:Label>
                                        </span><span class="widget-icon">
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Status/Icons/Resolved.png" /></span>
                                        <span class="widget-label ForeColor">Resolved</span></a>
                                </div>
                            </div>--%>
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
                            <th>Category</th>
                            <th>Subcategory</th>
                            <th>Assign To</th>
                            <th>Assignee</th>
                            <th>Closure Date & Time</th>
                            <th>Closure Findings</th>
                            <th>Total Resolution Time</th>
                            <th>Incident Description</th>
                        </tr>
                    </thead>
                    <tbody class="wfForm">
                    </tbody>
                </table>

            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="wfForm">
        <tr class="tr">
            <td>
                <input class="hfTicketStatusId" type="hidden" value='${TicketStatusId}' />
                <input class="hfReopen_Date" type="hidden" value='${Reopen_Date}' />
                <input class="hfTicketCode" type="hidden" value='${TicketCode}' />
                <label style="color: #1f2e4c;" class="Bold">${TicketCode}</label>
                <div class="cls_reopen" style="display: none;">
                    <strong>Re-Open Date : </strong>${Reopen_Date}
                </div>
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
            <td>${Category}</td>
            <td>${Subcategory}</td>
            <td>${AssignTo}</td>
            <td>${Assignee}</td>
            <td>${Ticket_CloseDateTime}</td>
            <td>${TypeOfComplaint}</td>
            <td>${TotalResolutionTime}</td>
            <td>${IncidentDescription_Closed}</td>
        </tr>
    </script>

    <script type="text/javascript">

        var Wallboard_Refresh_Time = $('.Wallboard_Refresh_TimeInSec').val();
        Wallboard_Refresh_Time = Wallboard_Refresh_Time * 1000;

        window.setInterval(function () {
            GetWallBoardData();
        }, Wallboard_Refresh_Time);
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


            SetDate();
            GetWallBoardData();
            $(".btn_Search").click(function () {
                GetWallBoardData();
                return false;
            });

            $(".btn_Cancel").click(function () {
                Cancel();
                return false;
            });
        }

        function Redirect(status) {

            //var dateFrom = new Date(document.getElementById("ContentPlaceHolder1_txtTicketDateFrom").value); 
            //var day_From = dateFrom.getDate();
            //var Month_From = dateFrom.getMonth() + 1;
            //var Year_From = dateFrom.getFullYear();
            //var FromDate = Month_From + "/" + day_From + "/" + Year_From


            //var dateTo = new Date(document.getElementById("ContentPlaceHolder1_txtTicketDateTo").value);
            //var day_To = dateTo.getDate();
            //var Month_To = dateTo.getMonth() + 1;
            //var Year_To = dateTo.getFullYear();
            //var ToDate = Month_To + "/" + day_To + "/" + Year_To


            var url = "/Pages/Tickets.aspx?StatusId=" + status;
            window.open(url, '_blank');
        }
    </script>
</asp:Content>

