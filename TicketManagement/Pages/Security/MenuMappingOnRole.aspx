<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="MenuMappingOnRole.aspx.cs" Inherits="Pages_Security_MenuMappingOnRole" %>

<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .TreeView {
            width: 500px;
        }

            .TreeView input[type="checkbox"] {
                width: 28px;
                height: 28px;
                background: #8C0A0A;
                background: linear-gradient(top, #8C0A0A 0%, #dfe5d7 40%, #b3bead 100%);
                box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            }

        TreeView a {
            font-size: 20px;
        }

        .TreeView a:active {
            background-color: green;
        }
    </style>



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
                    <li class="active">Security <span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/Security/MenuMappingOnRole.aspx">Menu Mapping on Role </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Menu Mapping on Role</h3>
                        </div>
                        <div class="widget-container">
                            <div class="controls-row">

                                <div class="control-group span4">
                                    <label class="control-label">Application</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlcompany" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" class="span12 ddlCompany " />
                                    </div>
                                </div>

                                <div class="control-group span4">
                                    <label class="control-label">Role</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlRole" class="span12 ddlRole DropDownValidate" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" Text="Update" ValidationGroup="ValidateOnSave" OnClientClick="return ControlsValidateFunction()" runat="server" CssClass="btn btn-danger" OnClick="btnUpdate_Click" />
                                </div>
                            </div>

                            <br />

                            <div class="controls-row btn-danger" id="divError_" runat="server" visible="false">
                                <div id="lblError_" runat="server"></div>
                            </div>

                            <br />
                            <br />

                            <div class="controls-row">

                                <div class="controls">
                                    <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All" CssClass="TreeView">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



    <script type="text/javascript">
        function pageLoad() {
            $(".btnAdd").click(function () {

                reset();

            });
            function reset() {
                $(".txtAdd").val('');
            }
        }
        function OpenModal() {
            $('.OpenModal').click();
        }

        function CloseModal() {
            $('.close').click();
        }

        function OpenDialog(a, b, c) {
            sweetAlert(a, b, c);
        }


        function ControlsValidateFunction() {

            var State = true;
            $(".DropDownValidate").each(function () {
                if ($(this).val() == "0" || $(this).val() == "") {
                    $(this).css("border-color", "Red");
                    State = false;

                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }

            });
            $(".TextBoxValidate").each(function () {

                if ($(this).val() == "") {

                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }

            }
            );
            return State;
        }


    </script>




</asp:Content>

