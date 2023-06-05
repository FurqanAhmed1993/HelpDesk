<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_ChangePassword" %>

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
                    <li><a href="/Pages/ChangePassword.aspx">Change Password </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Change Password</h3>
                        </div>



                        <div class="widget-container">
                            <div class="controls-row">

                                <div class="control-group span4">
                                    <label class="control-label">Old Password </label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" PlaceHolder="Old Password " CssClass="span12 TextBoxValidate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="controls-row">
                                <div class="control-group span4">
                                    <label class="control-label">New Password</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="NewPassword" PlaceHolder="New Password" TextMode="Password" CssClass="span12 TextBoxValidate"></asp:TextBox>



                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewPassword"
                                            ErrorMessage="New Password does not meet complexity requirements." ForeColor="Red" Display="Dynamic" CssClass="failureNotification"
                                            ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})"
                                            ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="controls-row">
                                <div class="control-group span4">
                                    <label class="control-label">Confirm New Password</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="ConfirmNewPassword" PlaceHolder="Confirm New Password" TextMode="Password" CssClass="span12 TextBoxValidate"></asp:TextBox>

                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup"></asp:CompareValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ConfirmNewPassword" ForeColor="Red"
                                            ErrorMessage="Confirm Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification"
                                            ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})"
                                            ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>


                                    </div>
                                </div>
                            </div>
                            <div class="controls-row">
                                <div class="control-group span4">
                                    <p style="color: red">
                                        (NOTE: Minimum size of password is 8 characters and it must include at least one non-alphabetic character.)
                                    </p>
                                </div>
                            </div>
                            <div class="controls-row">
                                <div class="control-group span4">
                                    <div class="pull-right">
                                        <asp:Button runat="server" CommandName="ChangePassword" ID="btn_ChangePassword" OnClick="btn_ChangePassword_Click" Text="Change Password" class="btn btn-danger btn_Search" ValidationGroup="ChangeUserPasswordValidationGroup" OnClientClick="return ControlsValidateFunction()"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <br />

                            <div class="controls-row btn-danger" id="divError_" runat="server" visible="false">
                                <div id="lblError_" runat="server"></div>
                            </div>
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>


    </div>

    <script type="text/javascript">


        function AlertBox(title, Message, type) {
            swal(title, Message, type);
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

