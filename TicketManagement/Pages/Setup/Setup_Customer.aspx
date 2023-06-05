<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Setup_Customer.aspx.cs" Inherits="Pages_Setup_Setup_Customer" %>

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
                    <li class="active">Setup <span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/Setup/Setup_Customer.aspx">Customer </a></li>
                </ul>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Customer</h3>
                        </div>


                        <div class="widget-container">
                            <div class="controls-row">
                                <div class="control-group span3">
                                    <label class="control-label">Customer Name</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtCustomerNameSearch" PlaceHolder="Customer Name" CssClass="span12 EnterEvent"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="control-group span3">
                                    <label class="control-label">Contact</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtContactSearch" PlaceHolder="Contact No" MaxLength="12" CssClass="span12 integers"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="control-label">Email</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtEmailSearch" PlaceHolder="Email" CssClass="span12"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <div class="pull-right" style="margin-top: 22px;">
                                        <asp:Button runat="server" ID="btn_Search" OnClick="btn_Search_Click" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                        <asp:Button runat="server" ID="btn_Cancel" OnClick="btn_Cancel_Click" Text="Cancel" class="btn btn-warning"></asp:Button>
                                    </div>
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
                            <button type="button" class="myModal btn btn-info pull-right btn_AddItem" data-toggle="modal" data-target="#myModal" style="margin-top: -35px; margin-right: 10px;">Add</button>
                        </div>
                        <div class="widget-container">

                            <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Customer Name</th>
                                        <th>Contact No </th>
                                        <th>Email</th>
                                        <th>Address</th>

                                        <th style="width: 170px; text-align: center;">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                </td>
                                                <td>
                                                    <input type="hidden" id="hfRId" value=' <%# Eval("CustomerId") %>' runat="server" />
                                                    <%# Eval("CustomerName") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ContactNo") %>
                                                </td>
                                                <td>
                                                    <%# Eval("EmailAddress") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Address") %>
                                                </td>

                                                <td class="project-actions" style="text-align: center;">
                                                    <asp:LinkButton ID="lbEdit"
                                                        runat="server"
                                                        CssClass="btn btn-primary" Text="Edit" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("CustomerId") %>'><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbDelete"
                                                        runat="server"
                                                        CssClass="btn btn-danger"
                                                        Text="Delete" ToolTip="Delete" CommandName="Delete"
                                                        OnClientClick="return sweetAlertConfirm(this,'Are you sure you want to delete?');"
                                                        CommandArgument='<%# Eval("CustomerId") %>'><span aria-hidden="true" class="fa fa-trash"></span>Delete
                                                    </asp:LinkButton>
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


        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">

                    <div class="modal-body">
                        <div class="row-fluid">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <input type="hidden" id="hfId" runat="server" class="hfId" />
                                    <div class="content-widgets light-gray">
                                        <div class="widget-head bluePSW">
                                            <h3>Add /Edit  Customer</h3>
                                        </div>
                                        <div class="widget-container">
                                            <div class="controls-row">

                                                <div class="control-group span6">
                                                    <label class="control-label">Customer Name</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtCustomerName" PlaceHolder="Customer Name" MaxLength="50" CssClass="span12 txtCustomerName TextBoxValidate"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group span6">
                                                    <label class="control-label">Contact No</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtContactNo" PlaceHolder="Contact No" MaxLength="12" CssClass="span12 integers txtContactNo TextBoxValidate"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="controls-row">
                                                <div class="control-group span6">
                                                    <label class="control-label">Email</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtEmail" PlaceHolder="Email" MaxLength="30" CssClass="span12 txtEmail"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group span6">
                                                    <label class="control-label">Address</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtAddress" PlaceHolder="Address" MaxLength="100" CssClass="span12"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="controls-row" style="margin-bottom: 20px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_Add" OnClick="btn_Add_Click" Text="Add" ValidationGroup="ValidateOnSave" class="btn btn-danger" OnClientClick="return ControlsValidateFunction()"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_Close" Text="Close" OnClick="btn_Close_Click" class="btn btn-warning"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="controls-row btn-danger" id="divError" runat="server" visible="false">
                                                <div id="lblError" runat="server"></div>
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                    <ProgressTemplate>
                                        <uc1:InProgress ID="InProgresss22" runat="server" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>

    </div>

    <script type="text/javascript">

        function pageLoad() {
            $(".btn_AddItem").click(function () {
                $('#myModal').zIndex(1050);
            });
            $(".integers").on("keypress", function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            });
            $('.integers').on('paste', function (event) {
                if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
                    event.preventDefault();
                }
            });

        }

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

        function ClosePopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function OpenPopup() {
            $('.btn_AddItem').click();
            $('#myModal').zIndex(1050);
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


            var NumberLength = $('.txtContactNo').val().length;
            var email = $('.txtEmail').val();
            const re = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

            if (!$('.txtCustomerName').val()) {
                AlertBox('Alert', 'Customer Name field is empty. Please fill out this field.', 'warning');
                return false;
            } else if (!$('.txtContactNo').val()) {
                AlertBox('Alert', 'Contact No field is empty. Please fill out this field.', 'warning');
                return false;
            } else if (NumberLength < 11) {
                AlertBox('Alert', 'Contact No is not valid.', 'warning');
                return false;
            }

            if (email !== "") {
                if (!re.test(email)) {
                    AlertBox('Alert', 'You have entered an invalid Email. Please enter valid Email.', 'warning');
                    return false;
                }
            }


            return State;
        }

        function sweetAlertConfirm(btnDelete, title, text) {

            if (btnDelete.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnDelete.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: title,//'Are you sure ? You want to delete',
                    text: text, //'You will not be able to recover this record..!',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#f83f37',
                    cancelButtonColor: '#eee',
                    confirmButtonText: 'Yes, Delete It',
                    cancelButtonText: 'No, Cancel'
                },
                    function () {
                        // Set data-confirmed attribute to indicate that the action was confirmed
                        btnDelete.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnDelete.click();
                    })
            }
        }
    </script>


</asp:Content>

