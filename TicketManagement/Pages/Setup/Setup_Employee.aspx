<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Setup_Employee.aspx.cs" Inherits="Pages_Setup_Setup_Employee" %>

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
                    <li><a href="/Pages/Setup/Setup_Employee.aspx">User </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <input runat="server" type="hidden" id="HFRolId" class="HFRolId" value="0" />
                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>User</h3>
                        </div>
                        <div class="widget-container">
                            <div class="controls-row">

                                <div class="control-group span3">
                                    <label class="control-label">Role</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlRole_Search" CssClass="span12 "></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="control-group span3">
                                    <label class="control-label">User Name</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtUsernameSearch" PlaceHolder="User Name" CssClass="span12 EnterEvent"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span3">
                                    <label class="control-label">Email / Login Id</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtLogininSearch" PlaceHolder="Email id " CssClass="span12 EnterEvent"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="control-group span3" style="margin-top: 30px">
                                    <div class="controls">
                                        <p style="color: red">
                                            Note : Default password is <asp:Label runat="server" ID="lblDefaultPassword" ></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>

                            <div class="controls-row">
                                <div class="pull-right">
                                    <asp:Button runat="server" ID="btn_Search" OnClick="btnSearch_Click" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                    <asp:Button runat="server" ID="btn_Cancel" OnClick="btnCancel_Click" Text="Cancel" class="btn btn-warning"></asp:Button>
                                </div>
                            </div>

                            <br />

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
                                        <th>Role</th>
                                        <th>User Name</th>
                                        <th>Email / Login Id</th>
                                        <th>Phone #</th>
                                        <th>Address</th>
                                        <th>Department</th>
                                        <%--<th>Designation</th>--%>
                                        <th>Enable / Disable
                                            <br />
                                            User</th>
                                        <th style="width: 170px; text-align: center;">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="hidden" id="hfRId" value=' <%# Eval("EmployeeCode") %>' runat="server" />
                                                    <input type="hidden" id="hfUserCode" value=' <%# Eval("UserCode") %>' runat="server" />
                                                    <input type="hidden" id="hfRoleId" value=' <%# Eval("RoleId") %>' runat="server" />
                                                    <%# Eval("Role") %>
                                                </td>
                                                <td>
                                                    <%# Eval("EmployeeName") %>
                                                </td>
                                                <td>
                                                    <%# Eval("EmailId") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PhoneNo") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Address") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Department") %>
                                                </td>
                                               <%-- <td>
                                                    <%# Eval("Designation") %>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("IsEnable") %>
                                                </td>


                                                <td class="project-actions" style="text-align: center;">
                                                    <asp:LinkButton ID="lbEdit"
                                                        runat="server"
                                                        CssClass="btn btn-primary" OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbDelete"
                                                        runat="server"
                                                        CssClass="btn btn-danger" OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure you wants to delete?')"><span aria-hidden="true" class="fa fa-trash"></span>Delete
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
        <!-- Trigger the modal with a button -->

        <%-- style="width: 560px !important;"--%>
        <!-- Modal -->
        <div id="myModal" style="width: 600px !important" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
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
                                            <h3>Add / Edit User</h3>
                                        </div>
                                        <div class="widget-container">

                                            <div class="controls-row">

                                                <div class="control-group span4">
                                                    <label class="control-label">Role</label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlRole" CssClass="span12 ddlRole DropDownValidate"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span4">
                                                    <label class="control-label">Email / Login Id</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtEmailId" TextMode="Email" PlaceHolder="Email / Login Id" MaxLength="30" CssClass="span12 txtEmailId TextBoxValidate"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div class="control-group span4 DivUserName">
                                                    <label class="control-label">User Name</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtemloyeeName" PlaceHolder="User Name" MaxLength="45" CssClass="span12 txtemloyeeName TextBoxValidate"></asp:TextBox>
                                                    </div>
                                                </div>


                                            </div>

                                            <div runat="server" id="Employee" class="controls-row Employee">
                                                <div class="controls-row">

                                                    <div class="control-group span4">
                                                        <label class="control-label">Phone #</label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtPhoneNo" PlaceHolder="Phone No" MaxLength="12" CssClass="span12  txtPhoneNo integers"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="control-group span4">
                                                        <label class="control-label">Department</label>
                                                        <div class="controls">
                                                           
                                                            <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="span12 ddlDepartment "></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                   <%-- <div class="control-group span4">
                                                        <label class="control-label">Designation</label>
                                                        <div class="controls">
                                                            <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="span12 ddlDesignation "></asp:DropDownList>
                                                        </div>
                                                    </div>--%>

                                                </div>

                                                <div class="controls-row">

                                                    <div class="control-group span12">
                                                        <label class="control-label">Address</label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtAddress" PlaceHolder="Address" CssClass="span12 txtAddress"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="controls-row">
                                                <div class="control-group span4">
                                                    <label class="control-label">Enable / Disable User</label>
                                                    <div class="controls">
                                                        <asp:CheckBox runat="server" ID="chk_Enable_Disable" Text="" Checked="true" />
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="controls-row" style="margin-bottom: 20px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_Add" OnClick="btnAdd_Click" Text="Add" ValidationGroup="ValidateOnSave" class="btn btn-danger" OnClientClick="return ControlsValidateFunction()"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_Close" Text="Close" OnClick="btnClose_Click" class="btn btn-warning"></asp:Button>
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

            $('.cblCheckAll input').change(function () {
                var currChk = $(this);
                if ($(this).val() == "0") {
                    $(this).closest('table').find('input:checkbox').prop('checked', $(currChk).is(':checked'));
                }
                else {
                    var allCheckboxCount = $(this).closest('table').find('input:checkbox').size();
                    var allCheckedCount = $(this).closest('table').find('input:checkbox:checked').not('input:checkbox[value=0]').size();
                    var isChecked = false;
                    if (allCheckedCount >= allCheckboxCount - 1) {
                        isChecked = true;
                    }
                    $(this).closest('table').find('input:checkbox[value=0]').prop('checked', isChecked);
                }
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
            //width: '60%';
            $('.btn_AddItem').click();
            $('#myModal').zIndex(1050);
        }

        function ControlsValidateFunction() {
            var State = true;

            $(".DropDownValidate").each(function () {
                if ($(this).val() == "0" || $(this).val() == "") {
                    $(this).css("border-color", "Red");
                    if (State == true) {
                        State = false;
                    }

                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }

            });
            $(".TextBoxValidate").each(function () {

                if ($(this).val() == "") {

                    $(this).css("border-color", "Red");
                    if (State == true) {
                        State = false;
                    }
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }

            });

            var NumberLength = $('.txtPhoneNo').val().length;

            if ($('.ddlRole').val() === "0") {
                AlertBox('Alert', 'Please select Role.', 'warning');
                return false;
            } else if (!$('.txtEmailId').val()) {
                AlertBox('Alert', 'Email/Login Id field is empty. Please fill out this field.', 'warning');
                return false;
            } else if (!$('.txtemloyeeName').val()) {
                AlertBox('Alert', 'User Name field is empty. Please fill out this field.', 'warning');
                return false;
            } else if ($('.ddlDepartment').val() === "0") {
                AlertBox('Alert', 'Please select Department.', 'warning');
                return false;
            }

            if ($('.txtPhoneNo').val() !== "") {
                if (NumberLength < 11) {
                    AlertBox('Invalid Phone Number', 'Phone Number is not valid.', 'warning');
                    return false;
                }
            }
            
           
         
            return State;
        }

    </script>


</asp:Content>

