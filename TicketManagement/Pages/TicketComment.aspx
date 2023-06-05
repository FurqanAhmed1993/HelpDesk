<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PopUpMaster.master" AutoEventWireup="true" CodeFile="TicketComment.aspx.cs" Inherits="Pages_TicketComment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/Comments.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
            <div class="row-fluid">
                <div class="content-widgets light-gray">
                    <div class="widget-head orange">
                        <h3>Ticket Comments </h3>
                    </div>
                    <div class="widget-container">
                        <div class="controls-row">

                            <div class="control-group span1">
                                <div class="controls">
                                    <img src="/images/user-pro.png" class="profile_pic circle" style="width: 50px; height: 50px;" />
                                </div>
                            </div>

                            <div class="control-group span11">
                                <div class="chatbox color_green">
                                    <textarea id="txtDescription" runat="server" class="txtDescription" name="test2" onkeyup="resizeTextarea('test2')" data-resizable="true"></textarea>
                                </div>

                            </div>
                        </div>

                        <div class="controls-row btn-danger" id="divError_" runat="server" visible="false">
                            <div id="lblError_" runat="server"></div>
                        </div>

                        <div class="controls-row Div_ButttonSave" style="margin-bottom: 10px;">
                            <div class="pull-right">
                                <asp:Button ID="btn_Save" runat="server" Text="Save" class="btn btn-danger btn_Save" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="wfForm">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/x-jQuery-tmpl" id="wfForm">
        <table class="table">
            <tr class="tr_">
                <td valign="top" style="width: 50px">
                    <img src="/images/user-pro.png" class="profile_pic circle" style="width: 50px; height: 50px; margin-left: 10px;" /><%--margin-left: 10px;--%>
                </td>
                <td>
                    <div class="chatbox color_gray" style="min-height: 60px; width: 93.5%; margin-left: 4%;">
                        <span>{{html Description}}
                        </span>
                    </div>

                    <span style="padding-left: 4%">${CommentBy}</span>
                    <span style="float: right; padding-right: 1.5%">${Date}    </span>

                </td>
            </tr>
        </table>
    </script>


    <script type="text/javascript">

        function pageLoad() {

            OnLoad();
            $(".btn_Save").click(function () {
                Save();
                return false;
            });
        }




        function ControlsValidateFunction() {

            var State = false;
            var Desc = $(".txtDescription").val();
            if (Desc != "") {
                State = true;
            }
            return State;
        }



    </script>



</asp:Content>

