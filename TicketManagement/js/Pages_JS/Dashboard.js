

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 10)];
    }
    return color;
}



function GetBarChart(Sym, Reference) {

    var service = new CyberTicketService.TicketService();
    service.BarChart(Sym, onGetBarChart, null, Reference);
}
function onGetBarChart(Result, Reference) {

    var res = jQuery.parseJSON(Result);
    Ref = Reference.split(",")[0];
    Title = Reference.split(",")[1];
    SetChartBar(res, Ref, Title);
}
function SetChartBar(Result, Reference, Title) {
    var ctx = document.getElementById(Reference).getContext('2d');
    var Labels = [];
    var Series = [];

    $(Result).each(function () {

        Labels.push(this.Label);
        Series.push(this.Count);

    });

    var Color = getRandomColor();


    //alert(Labels);
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Labels,
            datasets: [{
                label: Title,
                data: Series,
                backgroundColor: Color
            }
            ]
        }
    });
}


function GetPieChart(Sym, Reference) {

    var service = new CyberTicketService.TicketService();
    service.PieChart(Sym, onGetPieChart, null, Reference);
}
function onGetPieChart(Result, Reference) {
    var res = jQuery.parseJSON(Result);
    SetPieChart(res, Reference);
}
function SetPieChart(Result, Reference) {

    var Count = $(Result).length;
    var Colors = [];
    for (var i = 0; i < Count; i++) {
        Colors.push(getRandomColor());

    }
    var ctx = document.getElementById(Reference).getContext('2d');

    var Labels = [];
    var Series = [];

    $(Result).each(function () {
        Labels.push(this.Label);
        Series.push(this.Count);
    });

    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Labels,
            datasets: [{
                backgroundColor: Colors,
                data: Series
            }]
        }
    });
}

function GetpolarAreaChart(Sym, Reference) {
    var service = new CyberTicketService.TicketService();
    service.polarAreaChart(Sym, onGetpolarAreaChart, null, Reference);
}
function onGetpolarAreaChart(Result, Reference) {
    var res = jQuery.parseJSON(Result);
    SetpolarAreaChart(res, Reference);
}
function SetpolarAreaChart(Result, Reference) {

    var Count = $(Result).length;
    var Colors = [];
    for (var i = 0; i < Count; i++) {
        Colors.push(getRandomColor());

    }
    var ctx = document.getElementById(Reference).getContext('2d');

    var Labels = [];
    var Series = [];

    $(Result).each(function () {
        Labels.push(this.Label);
        Series.push(this.Count);
    });

    var myChart = new Chart(ctx, {
        type: 'polarArea',
        data: {
            labels: Labels,
            datasets: [{
                backgroundColor: Colors,
                data: Series
            }]
        }
    });
}



function GetRadarChart(Sym, Reference) {
    var service = new CyberTicketService.TicketService();
    service.doughnutChart(Sym, onGetRadarChart, null, Reference);
}
function onGetRadarChart(Result, Reference) {
    var res = jQuery.parseJSON(Result);
    SetpRadarChart(res, Reference);
}
function SetpRadarChart(Result, Reference) {

    var Count = $(Result).length;
    var Colors = [];
    for (var i = 0; i < Count; i++) {
        Colors.push(getRandomColor());

    }
    var ctx = document.getElementById(Reference).getContext('2d');
    ctx.canvas.height = 338;
    ctx.canvas.width = 1269;
    var Labels = [];
    var Series = [];

    $(Result).each(function () {
        Labels.push(this.Labels);
        Series.push(this.Count);
    });

    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Labels,
            datasets: [{
                backgroundColor: Colors,
                data: Series
            }]
        }
    });



    //var ctx = document.getElementById(Reference);
    //var Count = $(Result).length;
    //var Colors = [];
    //for (var i = 0; i < Count; i++) {
    //    Colors.push(getRandomColor());
    //}
    //var Labels = [];
    //var Series = [];
    //var Label = [...new Set(Result.map(item => item.Label))];

    //$(Result).each(function () {
    //    Labels.push(this.Labels);
    //    Series.push(this.Count);
    //});

    //var myChart = new Chart(ctx, {
    //    type: 'radar',
    //    data: {
    //        labels: Labels,
    //        datasets: [{
    //            label: Label,
    //            backgroundColor: Colors,
    //            borderColor: Colors,
    //            data: Series
    //        }]
    //    }
    //});


    ////var ctx = document.getElementById("radarProductSubCategory");
    ////var myChart = new Chart(ctx, {
    ////    type: 'radar',
    ////    data: {
    ////        labels: ["M", "T", "W", "T", "F", "S", "S"],
    ////        datasets: [{
    ////            label: 'apples',
    ////            backgroundColor: "rgba(153,255,51,0.4)",
    ////            borderColor: "rgba(153,255,51,1)",
    ////            data: [12, 19, 3, 17, 28, 24, 7]
    ////        }, {
    ////            label: 'oranges',
    ////            backgroundColor: "rgba(255,153,0,0.4)",
    ////            borderColor: "rgba(255,153,0,1)",
    ////            data: [30, 29, 5, 5, 20, 3, 10]
    ////        }]
    ////    }
    ////});

}




function GetdoughnutChart(Sym, Reference) {
    var service = new CyberTicketService.TicketService();
    service.doughnutChart(Sym, ondoughnutChart, null, Reference);
}
function ondoughnutChart(Result, Reference) {
    var res = jQuery.parseJSON(Result);
    SetdoughnutChart(res, Reference);
}
function SetdoughnutChart(Result, Reference) {

    var Count = $(Result).length;
    var Colors = [];
    for (var i = 0; i < Count; i++) {
        Colors.push(getRandomColor());

    }
    var ctx = document.getElementById(Reference).getContext('2d');

    var Labels = [];
    var Series = [];

    $(Result).each(function () {
        Labels.push(this.Label);
        Series.push(this.Count);
    });

    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: Labels,
            datasets: [{
                backgroundColor: Colors,
                data: Series
            }]
        }
    });
}

function GetLineChart(DateFrom, DateTo, Reference) {
    var service = new CyberTicketService.TicketService();
    service.LineChart(DateFrom, DateTo, onGetLineChart, null, Reference);
}
function onGetLineChart(Result, Reference) {
    var res = jQuery.parseJSON(Result);
    SetChartLine(res, Reference);
}
function SetChartLine(Result, Reference) {


    var ctx = document.getElementById(Reference).getContext('2d');

    var Labels = [];
    var Series = [];



    $(Result).each(function () {

        Labels.push(this.TrendDate.split("T")[0]);
        Series.push(this.TicketCount);

    });

    ctx.canvas.height = 300;
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: Labels,
            datasets: [{
                label: 'Ticket Count ',
                data: Series,
                backgroundColor: "rgb(128, 31, 60)"
            }]


        },
        options: {
            maintainAspectRatio: false,
            responsive: true,

        }
    });

}


function GetDashboardGraphs(DateFrom, DateTo) {
    var service = new CyberTicketService.TicketService();
    service.DashboardCharts(DateFrom, DateTo, onGetDashboardGraphs, null);
}
function onGetDashboardGraphs(Result) {

    var res = jQuery.parseJSON(Result);
    SetChartLine(res.Table, "LineTicketTrend");
    SetpolarAreaChart(res.Table1, "Bartop5Products");
    SetdoughnutChart(res.Table2, "BarProduct");
    SetpRadarChart(res.Table3, "radarProductSubCategory");
}

function GetCharts() {
    var DateFrom = $('.txtTicketDateFrom').val().trim();
    var DateTo = $('.txtTicketDateTo').val().trim();
    var dFrom = new Date(DateFrom);
    var dTo = new Date(DateTo);
    if (dFrom > dTo) {
        AlertBox('Date is Incorrect', 'Ticket Created Date To should be greater than Ticket Created Date From.', 'warning');
        return false;
    } else {
        GetDashboardGraphs(DateFrom, DateTo);
        //GetLineChart("LineTicketTrend");
        //GetpolarAreaChart("pr", "Bartop5Products");
        //GetdoughnutChart("cit", "BarProduct");
        //GetRadarChart("radarProductSubCategory", "radarProductSubCategory");


        //GetBarChart("us", "BarTop5Users,Users");
        //GetdoughnutChart("cus", "BarTop5Customers");
        //GetPieChart("pri", "PieTicketPriority");

    }
    //var ctx = document.getElementById('BarProduct').getContext('2d');
    //var myChart = new Chart(ctx, {
    //    type: 'line',
    //    data: {
    //        labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
    //        datasets: [{
    //            label: 'apples',
    //            data: [12, 19, 3, 17, 6, 3, 7],
    //            backgroundColor: "rgba(153,255,51,0.4)"
    //        }, {
    //            label: 'oranges',
    //            data: [2, 29, 5, 5, 2, 3, 10],
    //            backgroundColor: "rgba(255,153,0,0.4)"
    //        }]
    //    }
    //});
}



function Dashboard_GetUserList() {

    var service = new CyberTicketService.TicketService();
    service.Dashboard_GetUserList(onDashboard_GetUserList, null, null);
}

function onDashboard_GetUserList(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.wfForm').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);
    $(".tr").click(function () {
        $(".lblUserName").text($(this).find('.Name').text().trim());
        $(".Hf_Selected_UserId").val($(this).find('.HfUser_Code').val().trim());
        var divTbodyGoalFund = $('.MessageDiv').html('');
        $('.MessageDiv').append(divTbodyGoalFund);
        $('.DivChat').slideDown();
        Dashboard_GetAllChat();
    });
}

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

function send(msg) {
    var msgNew = '<strong>' + $('.Hf_LoginUserName').val() + ' Says </strong>' + ' : <br />' + msg;
    var dNow = formatAMPM(new Date());
    var Msg_ = '<div class="container1">' +
        '<p style="word-wrap: break-word;">' + msgNew + '</p>' +
        '<span class="time-right">' + dNow + '</span>' +
        '</div>';
    $('.MessageDiv').append(Msg_);
    ScrollDownDiv();
    Dashboard_InsertChatIntoDB(msg);
}

function Dashboard_InsertChatIntoDB(msg) {

    var service = new CyberTicketService.TicketService();
    var UserTo = $('.Hf_Selected_UserId').val();
    service.Dashboard_InsertChatIntoDB(msg, UserTo, onDashboard_InsertChatIntoDB, null, null);
}

function onDashboard_InsertChatIntoDB(result) {

    //var res = jQuery.parseJSON(result);
}

function ScrollDownDiv() {
    var elem = document.getElementById('MessageDiv');
    elem.scrollTop = elem.scrollHeight;
}

function Dashboard_GetUnreadChat() {
    var UserTo = $('.Hf_Selected_UserId').val();
    if (UserTo != "" && UserTo != "0") {
        var service = new CyberTicketService.TicketService();
        service.Dashboard_GetUnreadChat(UserTo, onDashboard_GetUnreadChat, null, null);
    }
}

function onDashboard_GetUnreadChat(result) {

    if (result != "") {
        var result_ = result.split('Split_');
        var res_Chat = jQuery.parseJSON(result_[1]);
        $(res_Chat).each(function (k, v) {
            $('.MessageDiv').append(v.Value);
            ScrollDownDiv();
        });
    }
}

function Dashboard_GetAllChat() {
    var UserTo = $('.Hf_Selected_UserId').val();
    if (UserTo != "" && UserTo != "0") {
        var service = new CyberTicketService.TicketService();
        service.Dashboard_GetAllChat(UserTo, onDashboard_GetAllChat, null, null);
    }
}

function onDashboard_GetAllChat(result) {
    if (result != "") {
        var res = jQuery.parseJSON(result);
        $(res).each(function (k, v) {
            $('.MessageDiv').append(v.Value);
        });
        ScrollDownDiv();
    }
}

function GetWallBoardData() {
    var service = new CyberTicketService.TicketService();
    var UserLoginId = $('.hf_UserLoginId').val();
    service.GetWallBoardData(UserLoginId, onGetWallBoardData, null, null);
}

function onGetWallBoardData(result) {
    if (result != "") {
        var result_ = result.split('Split_');
        var res_StatusCount = jQuery.parseJSON(result_[0]);
        var res_TicketChats = jQuery.parseJSON(result_[2]);
        var res_TicketPrivateChatCount = jQuery.parseJSON(result_[3]);


        //$('.lblCancel').text(res_StatusCount[0].Cancel);
        //$('.lblClosed').text(res_StatusCount[0].Closed);
        //$('.lblInProgress').text(res_StatusCount[0].InProgress);
        //$('.lblJunk').text(res_StatusCount[0].Junk);
        //$('.lblNew').text(res_StatusCount[0].New);
        //$('.lblOnHold').text(res_StatusCount[0].OnHold);
        //$('.lblReopen').text(res_StatusCount[0].ReOpen);
        //$('.lblResolved').text(res_StatusCount[0].Resolved);
        $('.lblUnreadMessageResponseOverickets').text(res_TicketChats[0].ChatNotificationCount);
        $('.lblUnreadMessageInternalChatOverTickets').text(res_TicketChats[0].InternalChatNotificationCount);
        var PrivateChat = "You have " + res_TicketPrivateChatCount[0].TotalChatCount + " Message(s) From " + res_TicketPrivateChatCount[0].TotalUsers + " User(s)";
        $('.lblPrivateChatCount').text(PrivateChat);
    }
    else {

        //$('.lblCancel').text('0');
        //$('.lblClosed').text('0');
        //$('.lblInProgress').text('0');
        //$('.lblJunk').text('0');
        //$('.lblNew').text('0');
        //$('.lblOnHold').text('0');
        //$('.lblReopen').text('0');
        //$('.lblResolved').text('0');

        $('.lblUnreadMessageResponseOverickets').text('0');
        $('.lblUnreadMessageInternalChatOverTickets').text('0');
    }
}


function SetDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm == 1) {
        mm = "Jan";
    } else if (mm == 2) {
        mm = "Feb";
    } else if (mm == 3) {
        mm = "Mar";
    } else if (mm == 4) {
        mm = "Apr";
    } else if (mm == 5) {
        mm = "May";
    } else if (mm == 6) {
        mm = "Jun";
    } else if (mm == 7) {
        mm = "Jul";
    } else if (mm == 8) {
        mm = "Aug";
    } else if (mm == 9) {
        mm = "Sep";
    } else if (mm == 10) {
        mm = "Oct";
    } else if (mm == 11) {
        mm = "Nov";
    } else if (mm == 12) {
        mm = "Dec";
    }
    var Date_ = mm + " " + dd + "," + yyyy;

    $('.txtTicketDateFrom').val(Date_);
    $('.txtTicketDateTo').val(Date_);

}
