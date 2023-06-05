<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="Setup_Priority.aspx.cs" Inherits="Pages_Setup_Setup_Priority" %>

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
                    <li class="active">Dictionary Setup<span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li><a href="/Pages/Setup/Setup_Priority.aspx">Priority </a></li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <div class="row-fluid">

                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Priority</h3>
                        </div>
                        <div class="widget-container">
                            <div class="controls-row">
                              
                                <div class="control-group span4">
                                    <label class="control-label">Priority</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtPrioritySearch" PlaceHolder="Priority " CssClass="span12 EnterEvent"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="controls-row">
                                <div class="pull-right">
                                    <asp:Button runat="server" ID="btn_Search" OnClick="btnSearch_Click" Text="Search" class="btn btn-danger btn_Search"></asp:Button>
                                    <asp:Button runat="server" ID="btn_Cancel" OnClick="btnCancel_Click" Text="Cancel" class="btn btn-warning"></asp:Button>
                                </div>
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
                                        <th>Priority</th>
                                        <th style="width: 170px; text-align: center;">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                      <input type="hidden" id="hfRId" value=' <%# Eval("PriorityId") %>' runat="server" />
                                                    <%# Eval("Priority") %>
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
            <Triggers>

                <%-- <asp:PostBackTrigger ControlID="btn_Search" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <!-- Trigger the modal with a button -->


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
                                            <h3>Add /Edit  Priority</h3>
                                        </div>
                                        <div class="widget-container">
                                            <div class="controls-row">
                                            
                                                <div class="control-group span6">
                                                    <label class="control-label">Priority</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtPriority" PlaceHolder="Priority" MaxLength="50" CssClass="span12 TextBoxValidate"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="controls-row">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_Add" OnClick="btnAdd_Click" Text="Add" ValidationGroup="ValidateOnSave" class="btn btn-danger" OnClientClick="return ControlsValidateFunction()"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_Close" Text="Close" OnClick="btnClose_Click" class="btn btn-warning"></asp:Button>

                                                </div>

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
        }






        function reset() {
            //alert();
            $(".ddlCompany").val('0');
            $(".txtPriority").val('');
            $(".hfId").val('');
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
            return State;
        }


    </script>



</asp:Content>

