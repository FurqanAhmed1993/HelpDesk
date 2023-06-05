<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE HTML>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>PSW Helpdesk</title>

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Admin Panel Template">
    <meta name="author" content="Westilian: Kamrujaman Shohel">
    <!-- styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link rel="stylesheet" href="css/font-awesome.css">
    <link href="css/styles.css" rel="stylesheet">
    <link href="css/aristo-ui.css" rel="stylesheet">
    <link href="css/elfinder.css" rel="stylesheet">

    <link rel="shortcut icon" href="ico/favicon.png">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="ico/apple-touch-icon-57-precomposed.png">
    <!--============j avascript===========-->
    <script src="js/jquery.js"></script>
    <script src="js/jquery-ui-1.10.1.custom.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        body {
            background-color: #2d3f61;
        }

        .red {
            background-color: #1f2e4c;
        }

        .long-ribbon {
            background-color: #1f2e4c;
        }
    </style>
</head>
<body>
    <div class="layout">
        <!-- Navbar================================================== -->
        <div class="navbar navbar-inverse top-nav">
            <div class="navbar-inner">
                <div class="container">
                    <div class="btn-toolbar pull-right notification-nav">
                        <div class="btn-group">
                            <div class="dropdown">
                                <%-- <a class="btn btn-notification"><i class="icon-reply"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">


            <form class="form-signin-ribbon" id="form1" runat="server">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <img src="images/Logo_PSW.png" height="" />
                        <br />
                        <br />
                        <div class="form-group" runat="server" id="divform">

                            <div class="content-widgets gray">
                                <div class=" login-head red">
                                    <h3 class="form-signin-heading">Please sign in</h3>
                                </div>

                                <div class="long-ribbon">
                                    <div class="ribbon-content">

                                        <asp:Login ID="login1" runat="server" TitleText="" OnAuthenticate="login1_Authenticate" Width="100%"
                                            DisplayRememberMe="false" DestinationPageUrl="~/Default.aspx">
                                            <LayoutTemplate>

                                                <div class="controls input-icon">
                                                    <i class=" icon-user-md"></i>
                                                    <asp:TextBox ID="UserName" runat="server" CssClass="input-block-level" placeholder="Login Id" required></asp:TextBox>
                                                </div>
                                                <div class="controls input-icon">
                                                    <i class=" icon-key"></i>
                                                    <asp:TextBox ID="Password" runat="server" CssClass="input-block-level" TextMode="Password" placeholder="Password" required></asp:TextBox>
                                                </div>
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-inverse btn-block" Text="Sign-in" ValidationGroup="login1" />
                                            </LayoutTemplate>
                                        </asp:Login>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <br />
                                                <div style="text-align: center; background-color: rgb(250, 255, 189);">
                                                    <img alt="" class="PopUpUpdateProgressTemplateContent" src="Images/ajax-loader-bar(3).gif" />
                                                    <br />
                                                    <span style="color: black;">Signing in...</span>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div class="alert alert-danger" id="divError" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblErrorNew" Text="Wrong Login Id or Password"></asp:Label>
                                        </div>
                                        <br />
                                        ©
                                        <asp:Label runat="server" ID="lblYear" Text="0"></asp:Label>, Sybrid Pvt Ltd. All rights reserved.
                                    </div>
                                </div>
                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>

        </div>
    </div>
</body>
</html>
