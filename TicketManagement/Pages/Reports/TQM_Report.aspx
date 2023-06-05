<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" AutoEventWireup="true" CodeFile="TQM_Report.aspx.cs" Inherits="Pages_Reports_TQM_Report" %>

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
                            <h3>TQM Report</h3>
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
                                        <asp:TextBox ID="txtTicketDateFrom" runat="server" placeHolder="Ticket Created Date From" CssClass="span12 datePicker txtTicketDateFrom"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">
                                        Ticket Created Date To
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Ticket Created Date To" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                             Display="Dynamic" ControlToValidate="txtTicketDateTo"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicketDateTo" runat="server" placeHolder="Ticket Created Date To" CssClass="span12 datePicker txtTicketDateTo"></asp:TextBox>
                                    </div>
                                </div>




                                <div class="control-group span2">
                                    <label class="control-label">Priority</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlPriority" CssClass="span12 ddlPriority"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Request Type</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="span12 ddlRequestType"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Category</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="span12 ddlCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="control-group span2">
                                    <label class="control-label">Subcategory</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlSubcategory" CssClass="span12 ddlSubcategory"></asp:DropDownList>
                                    </div>
                                </div>


                            </div>

                            <div class="controls-row">
                                <div class="control-group span2">
                                    <label class="control-label">Ticket No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTicket" runat="server" placeHolder="Ticket No" CssClass="span12  txtTicket"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Customer Name</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCustomerName" runat="server" placeHolder="Customer Name" CssClass="span12  txtCustomerName"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Method of Contact</label>
                                    <div class="controls">
                                        <asp:DropDownList runat="server" ID="ddlRequestMode" CssClass="span12 ddlRequestMode"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Customer Contact No</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtContactNo" runat="server" placeHolder="Customer Contact No" CssClass="span12  txtContactNo"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group span2">
                                    <label class="control-label">Customer Email</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtEmail" runat="server" placeHolder="Customer Email" CssClass="span12  txtEmail"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="controls-row">
                                <div class="control-group span6">
                                    <label class="control-label">Status</label>
                                    <div style="margin-top: -15px">
                                        <asp:CheckBoxList runat="server" ID="Chk_Status" RepeatDirection="Horizontal" CellPadding="10" CellSpacing="10" CssClass="cblCheckAll"></asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="control-group span6">
                                    <div class="pull-right" style="margin-top: 35px;">
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
                    <div class="content-widgets light-gray" style="overflow-x: auto;">
                        <div class="widget-head bluePSW">
                            <h3>Total Records :
                                <asp:Label runat="server" ID="TotalRecords" CssClass="TotalRecords" Text="0"></asp:Label></h3>
                        </div>
                        <asp:Repeater ID="rpt"  runat="server">
                            <HeaderTemplate>
                                <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable" border="1">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Ticket No</th>
                                            <th>Customer</th>
                                            <th>Contact No</th>
                                            <th>Alternative Contact No</th>
                                            <th>Status</th>
                                            <th>Escalated to Level 2 Department</th>
                                            <th>Ticket Creation Date & Time </th>
                                            <th>Title</th>
                                            <th>Contact Method</th>
                                            <th>Request Type</th>
                                            <th>Category</th>
                                            <th>Sub Category</th>
                                            <th>Assign To</th>
                                            <th>Assignee</th>
                                            <th>Closure Date & Time</th>
                                            <th>Closure Findings</th>
                                            <th>Total Resolution Time</th>
                                            <th>Incident Description</th>
                                            
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 50px; text-align: left">
                                        <%--<%# Eval("SerialNo") %>--%>
                                        <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                       
                                    </td>

                                    <td>
                                        <%# Eval("TicketCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                      <td>
                                        <%# Eval("ContactNo") %>
                                    </td>
                                      <td>
                                        <%# Eval("AlternativeNumber") %>
                                    </td>
                                    <td>
                                        <%# Eval("Status") %>
                                    </td>
                                         <td>
                                        <%# Eval("IsEscalatedToL2") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(Eval("TicketCreationDatetime"))!="" ? Convert.ToDateTime(Eval("TicketCreationDatetime")).ToString(Constant.DateTimeFormat1)  : ""%>
                                    </td>
                                    <td>
                                        <%# Eval("TicketTitle") %>
                                    </td>
                                    <td>
                                        <%# Eval("TicketRequestMode") %>
                                    </td>
                                    <td>
                                        <%# Eval("RequestType") %>
                                    </td>
                                    <td>
                                        <%# Eval("Category") %>
                                    </td>
                                    <td>
                                        <%# Eval("Subcategory") %>
                                    </td>
                                    <td>
                                        <%# Eval("AssignTo") %>
                                    </td>
                                    <td>
                                        <%# Eval("Assignee") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(Eval("Ticket_CloseDateTime"))!="" ? Convert.ToDateTime(Eval("Ticket_CloseDateTime")).ToString(Constant.DateTimeFormat1)  : ""%>
                                    </td>
                                    <td>
                                        <%# Eval("TypeOfComplaint") %>
                                    </td>
                                    <td>
                                        <%# Eval("TotalResolutionTime") %>
                                    </td>
                                    <td>
                                        <%# Eval("IncidentDescription_Closed") %>
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

