<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="DeletedTickets.aspx.cs" Inherits="Pages_Reports_DeletedTickets" %>

<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <uc1:InProgress ID="InProgress2" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div class="container-fluid">
        <div class="row-fluid ">
            <div class="span12">
                <div class="primary-head">
                    <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                </div>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row-fluid">
                    <div class="content-widgets light-gray">
                        <div class="widget-head bluePSW">
                            <h3>Deleted Tickets Report</h3>
                        </div>
                        <div class="widget-container">

                            <div class="controls-row">
                                <div class="control-group span2">
                                    <label class="control-label">
                                        Ticket Created Date From
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="Ticket Created Date From" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                             Display="Dynamic" ControlToValidate="txtTicketDateFrom"></asp:RequiredFieldValidator>
                                    </label>

                                    <div class="controls">
                                        <asp:TextBox ID="txtTicketDateFrom" runat="server" placeHolder="Ticket Created Date From" CssClass="span12 datePicker txtTicketDateFrom"  AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">
                                        Ticket Created Date To
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Ticket Created Date To" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                             Display="Dynamic" ControlToValidate="txtTicketDateTo"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicketDateTo" runat="server" placeHolder="Ticket Created Date To" CssClass="span12 datePicker txtTicketDateTo" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="control-group span8">
                                    <div class="pull-right" style="margin-top: 25px;">
                                        <asp:Button runat="server" ID="btn_Search" Text="Search" OnClick="btn_Search_Click" class="btn btn-danger btn_Search" ValidationGroup="Search"></asp:Button>
                                        <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" OnClick="btn_Cancel_Click" class="btn btn-warning btn_Cancel"></asp:Button>
                                        <asp:Button runat="server" ID="btn_Export" Text="Export To Excel" Visible="false" OnClick="btn_Export_Click" class="btn btn-info btn_Export"></asp:Button>
                                    </div>
                                </div>
                            </div>


                            <div class="controls-row btn-danger" id="divError" runat="server" visible="false">
                                <div id="lblError" runat="server"></div>
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
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <%--<table style="width: 100%; overflow-x: scroll; border-collapse: collapse" class="table table-hover" border="1">--%>
                                <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable" border="1">
                                    <thead>
                                        <tr>
                                            <th>Sr.No</th>
                                            <th>Ticket No</th>
                                            <th>Created On</th>
                                            <th>Ticket Title</th>
                                            <th>Remarks</th>
                                            <th>Deleted By</th>
                                            <th>Deleted On</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 50px; text-align: left">
                                        <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                    </td>
                                    <td>
                                        <%# Eval("Ticket No") %>
                                    </td>

                                    <td>
                                        <%# Eval("Created On") %>
                                    </td>

                                    <td>
                                        <%# Eval("Ticket Title") %>
                                    </td>
                                     <td>
                                        <%# Eval("Description") %>
                                    </td>
                                    <td>
                                        <%# Eval("Deleted By") %>
                                    </td>
                                    <td>
                                        <%# Eval("Deleted On") %>
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                    </table>
                            </FooterTemplate>
                        </asp:Repeater>


                    </div>
                </div>


            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_Export" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <script type="text/javascript">

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
        }
    </script>

</asp:Content>


