<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/Dashboard.js"></script>
    <script src="js/Chart.min.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .BlackColor {
            color: black;
        }

        .main-wrapper {
            min-height: 500px !important;
        }
        /* Chat containers */
        .container1 {
            border: 2px solid #dedede;
            /*background-color: #f1f1f1;*/
            border-radius: 5px;
            padding: 10px;
            margin: 10px 0;
        }

        /* Darker chat container */
        .darker {
            border-color: #ccc;
            background-color: #ddd;
        }

        /* Clear floats */
        .container1::after {
            content: "";
            clear: both;
            display: table;
        }

        /* Style images */
        .container1 img {
            float: left;
            max-width: 60px;
            width: 100%;
            margin-right: 20px;
            border-radius: 50%;
        }

            /* Style the right image */
            .container1 img.right {
                float: right;
                margin-left: 20px;
                margin-right: 0;
            }

        /* Style time text */
        .time-right {
            float: right;
            color: #aaa;
        }

        /* Style time text */
        .time-left {
            float: left;
            color: #999;
        }
    </style>
    <%--  <div style="background-color: #2d3f61;">
        <div class="row-fluid ">
            <div class="span12">
                <div class="primary-head">
                    <div class="span12">
                        <img src="images/Logo_PSW.png" style="height: 50px;" />
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="main-wrapper">
        <%--    <input type="hidden" id="Dashboard_Refresh_TimeInSec" class="Dashboard_Refresh_TimeInSec" runat="server" value="3" />
        <input type="hidden" id="Wallboard_Refresh_TimeInSec" class="Wallboard_Refresh_TimeInSec" runat="server" value="5" />
        <input type="hidden" runat="server" value="" id="Hf_LoginUserName" class="Hf_LoginUserName" />
        <input type="hidden" runat="server" value="" id="hf_UserLoginId" class="hf_UserLoginId" />
        <input type="hidden" runat="server" value="" id="hf_IsCustomer" class="hf_IsCustomer" />--%>
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
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="row-fluid ">
                        <div class="span6">
                            <div class="board-widgets dark-yellow">
                                <div class="board-widgets-head clearfix">
                                    <h4 class="pull-left"><i class=" icon-comment"></i>Private Chat</h4>
                                </div>
                                <div class="board-widgets-content">
                                    <span class="n-counter" style="font-size: 30px">
                                        <asp:Label runat="server" ID="lblPrivateChatCount" Text="0" CssClass="lblPrivateChatCount"></asp:Label></span><span class="n-sources">
                                        </span>
                                </div>
                                <div class="board-widgets-botttom">
                                    <a>Unread Messages</a>
                                </div>
                            </div>
                        </div>
                        <div class="span3 div_Dashboard">

                            <div class="board-widgets blue-violate">
                                <div class="board-widgets-head clearfix">
                                    <h4 class="pull-left"><i class=" icon-comment"></i>Internal Chat Over Tickets</h4>
                                </div>
                                <div class="Td_ICOT">
                                    <div class="board-widgets-content">
                                        <span class="n-counter" style="font-size: 50px">
                                            <asp:Label runat="server" ID="lblUnreadMessageInternalChatOverTickets" CssClass="lblUnreadMessageInternalChatOverTickets" Text="0"></asp:Label></span><span class="n-sources"></span>
                                    </div>
                                    <div class="board-widgets-botttom">
                                        <a>Unread  Comments</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="span3 div_Dashboard">
                            <div class="board-widgets green">
                                <div class="board-widgets-head clearfix">
                                    <h4 class="pull-left"><i class=" icon-inbox"></i>Response Over Tickets</h4>
                                </div>
                                <div class="Td_ROT">
                                    <div class="board-widgets-content">
                                        <span class="n-counter" style="font-size: 50px">
                                            <asp:Label runat="server" ID="lblUnreadMessageResponseOverickets" CssClass="lblUnreadMessageResponseOverickets" Text="0"></asp:Label></span><span class="n-sources"></span>
                                    </div>
                                    <div class="board-widgets-botttom">
                                        <a>Unread Messages</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
            <div class="row-fluid div_Dashboard" style="display:none;">
                <div class="span12">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Tickets Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="LineTicketTrend"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid div_Dashboard">
                <div class="span6">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Finding Tickets Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="Bartop5Products"></canvas>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Product Tickets Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="BarProduct"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid div_Dashboard">
                <div class="span12">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Product Sub-Category Tickets Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="radarProductSubCategory" style="width: 1269px; height: 338px;"></canvas>
                        </div>
                    </div>
                </div>

                <%--<div class="span6" style="display: none;">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Priority Wise Ticket Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="PieTicketPriority"></canvas>
                        </div>
                    </div>
                </div>--%>
            </div>
            <%-- <div class="row-fluid div_Dashboard" style="display: none;">
                <div class="span6">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Top 5 Users Wise Closed Ticket Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="BarTop5Users"></canvas>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="content-widgets light-gray">
                        <div class="widget-head blue">
                            <h3>Top 5 Customers Wise Ticket Count</h3>
                        </div>
                        <div class="widget-container">
                            <canvas id="BarTop5Customers"></canvas>
                        </div>
                    </div>
                </div>
            </div>--%>
            <%-- <div class="row-fluid div_Dashboard">

                   <div class="span6">
                            <div class="content-widgets light-gray">
                                <div class="widget-head blue">
                                    <h3>Top 5 Cities Wise Ticket Count</h3>
                                </div>
                                <div class="widget-container">
                                    <canvas id="barTop5Cities"></canvas>
                                </div>
                            </div>
                        </div>
            </div>--%>
            <%--  <div class="row-fluid">
                    <div class="span6">
                        <div class="content-widgets white">
                            <div class="widget-head light-blue" style="background-color: #8C0A0A">
                                <h3>Chat User's
                                </h3>
                            </div>
                            <br />
                            <input type="text" id="txtSearch" class="txtSearch" onkeyup="Searching()" placeholder="Search.." style="margin-left: 20px;" />

                            <div class="widget-container" style="overflow-x: scroll; height: 400px; overflow-y: scroll">
                                <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Region</th>
                                            <th>Name</th>
                                            <th>Email</th>
                                            <th>PhoneNo</th>
                                        </tr>
                                    </thead>
                                    <tbody class="wfForm">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="span6">
                        <div class="content-widgets white">
                            <div class="widget-head light-blue" style="background-color: #8C0A0A">
                                <h3>
                                    <i class="icon-comments-alt"></i>
                                    <asp:Label ID="lblUserName" runat="server" CssClass="lblUserName" Text="Select a User To Chat"></asp:Label>
                                    <input type="hidden" class="Hf_Selected_UserId" value="0" />
                                    <input type="image" src="/Images/close-button.png" style="background-color: #8C0A0A" class="btn btn-info pull-right btnChatClose" />
                                </h3>
                            </div>
                            <div class="widget-container DivChat" style="display: none;">
                                <div class="tab-widget tabbable tabs-left chat-widget ">
                                    <div id="MessageDiv" class="tab-content MessageDiv" style="height: 300px;">
                                    </div>
                                    <div class="chat-input">
                                        <textarea id="txtMSG" class="chat-inputbox span12 txtMSG" style="resize: none" placeholder="Type a message here ..." name="input"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
        </div>
    </div>

    <%--  <script type="text/x-jQuery-tmpl" id="wfForm">
        <tr class="tr">
            <td class="region">${Region}
                <input class="HfUser_Code" type="hidden" value='${User_Code}' />
            </td>
            <td class="Name">${Name}</td>
            <td class="Email">${Email}</td>
            <td class="PhoneNo">${PhoneNo}</td>
        </tr>
    </script>--%>

    <script>
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
            GetCharts();
            $(".btn_Search").click(function () {
                GetCharts();
                return false;
            });

            $(".btn_Cancel").click(function () {
                SetDate();
                GetCharts();
                return false;
            });
        }

        //var Wallboard_Refresh_TimeInSec = $('.Wallboard_Refresh_TimeInSec').val();
        //Wallboard_Refresh_TimeInSec = Wallboard_Refresh_TimeInSec * 1000;
        //var Dashboard_Refresh_TimeInSec = $('.Dashboard_Refresh_TimeInSec').val();
        //Dashboard_Refresh_TimeInSec = Dashboard_Refresh_TimeInSec * 1000;
        //GetWallBoardData();
        //Dashboard_GetUserList();
        //var IsCustomer = $('.hf_IsCustomer').val();
        //if (IsCustomer == "1") {
        //    $('.div_Dashboard').hide();
        //}
        //else {
        //    $(".Td_ROT").click(function () {
        //        var ResponseOverTicketsCount = $('.lblUnreadMessageResponseOverickets').text();
        //        if (ResponseOverTicketsCount > 0) {
        //            var href = $(this).attr('data-href');
        //            var link = $('<a href="/Pages/Tickets.aspx?ROT=1" />');
        //            link.attr('target', '_blank');
        //            window.open(link.attr('href'));
        //        }
        //    });
        //    $(".Td_ICOT").click(function () {
        //        var InternalChatOverTicketsCount = $('.lblUnreadMessageInternalChatOverTickets').text();
        //        if (InternalChatOverTicketsCount > 0) {
        //            var href = $(this).attr('data-href');
        //            var link = $('<a href="/Pages/Tickets.aspx?ICOT=1" />');
        //            link.attr('target', '_blank');
        //            window.open(link.attr('href'));
        //        }
        //    });

        //window.setInterval(function () {
        //    GetCharts();
        //}, Dashboard_Refresh_TimeInSec);
        //}

        //$(".btnChatClose").click(function () {
        //    $('.lblUserName').text('Select a User To Chat');
        //    $('.Hf_Selected_UserId').val('0');
        //    var divTbodyGoalFund = $('.MessageDiv').html('');
        //    $('.MessageDiv').append(divTbodyGoalFund);
        //    $('.DivChat').slideUp();
        //    return false;
        //});
        //$("#txtMSG").keypress(function (event) {
        //    if (event.which == 13) {
        //        event.preventDefault();
        //        var text = $(this).val();
        //        if (text !== "") {
        //            send(text);
        //            $(this).val('');
        //        }
        //    }
        //});
        //function Searching() {
        //    var input, filter, table, tr, td, i;
        //    input = $(".txtSearch");
        //    filter = input.val().toUpperCase();
        //    table = $(".wfForm");
        //    table.find('tr').each(function () {
        //        var Region = $(this).find('.region').text().trim();
        //        var Name = $(this).find('.Name').text().trim();
        //        var Email = $(this).find('.Email').text().trim();
        //        var PhoneNo = $(this).find('.PhoneNo').text().trim();
        //        if (Region.toUpperCase().indexOf(filter) > -1 || Name.toUpperCase().indexOf(filter) > -1 || Email.toUpperCase().indexOf(filter) > -1 || PhoneNo.toUpperCase().indexOf(filter) > -1) {
        //            $(this).show();
        //            return;
        //        }
        //        else {
        //            $(this).hide();
        //        }
        //    });
        //}
        //window.setInterval(function () {
        //    //Dashboard_GetUnreadChat();
        //    GetWallBoardData();
        //}, Wallboard_Refresh_TimeInSec);



    </script>




</asp:Content>

