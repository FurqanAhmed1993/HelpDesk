<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CreateTicket.aspx.cs" Inherits="Pages_CreateTicket" %>

<%@ Register Src="~/Controls/Shared/MultipleSelectionBox.ascx" TagPrefix="uc1" TagName="MultipleSelectionBox" %>

<%--<%@ Register Src="~/Controls/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>--%>
<%--<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="uc1" TagName="PagingAndSorting" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/Pages_JS/CreateTicket.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>

    <%-- <link rel="stylesheet" href="/Scripts/tagator/fm.tagator.jquery.css" />
    <script src="/Scripts/tagator/jquery-1.11.0.min.js"></script>
    <script src="/Scripts/tagator/fm.tagator.jquery.js"></script>--%>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid">

        <div class="row-fluid" runat="server" id="ClassSubheader">
            <div class="span12">
                <div class="primary-head">
                    <h3 class="page-header"><b>PSW Helpdesk</b></h3>
                </div>
                <ul class="breadcrumb">
                    <li><a href="/Default.aspx" class="icon-home"></a><span class="divider "><i class="icon-angle-right"></i></span></li>
                    <li class="active">Ticket <span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li>Initiate Ticket </li>
                    <%--<a href="/Pages/CreateTicket.aspx">  </a>--%>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input type="hidden" id="hfPhoneNumber" runat="server" class="hfPhoneNumber" value="" />
                <input type="hidden" id="hfEmailProductId" runat="server" class="hfEmailProductId" value="0" />
                <input type="hidden" id="hf_UserId" runat="server" class="hf_UserId" />
                <input type="hidden" id="hf_Validate" runat="server" class="hf_Validate" value="false" />
                <input type="hidden" id="hf_TicketMasterId" runat="server" class="hf_TicketMasterId" value="0" />
                <input type="hidden" id="hf_ManageSevicesMasterId" runat="server" class="hf_ManageSevicesMasterId" value="0" />
                <input type="hidden" id="hf_ControlFalse" runat="server" class="hf_ControlFalse" value="true" />
                <%--<input runat="server" type="hidden" id="hfCustomerId" class="hfCustomerId" value="0" />--%>
                <input runat="server" type="hidden" id="hfTicketCustomerId" class="hfTicketCustomerId" value="0" />
                <input runat="server" type="hidden" id="hfInitiatorId" class="hfInitiatorId" value="0" />
                <input runat="server" type="hidden" id="hfStatusId" class="hfStatusId" value="0" />

                <asp:Panel ID="pnlHideShow" runat="server">
                    <div class="row-fluid">

                        <div class="content-widgets light-gray SearchTicketPanel">
                            <div class="widget-head bluePSW">
                                <h3>Initiate Ticket </h3>
                            </div>
                            <div class="widget-container">
                                <div class="controls-row">
                                    <div class="control-group span12">
                                        <label style="color: #8C0A0A;">
                                            In order to search any customer, kindly enter contact no or email then click on search button. If you want to create new customer, checked the below checkbox and provide neccesary details then click on save.
                                        </label>
                                        <%--<asp:CheckBox ID="chkNewCustomer" Text="( In order to save new customer information kindly checked on checkbox )" Style="display: inline-flex;" runat="server"  />--%>
                                        <input type="checkbox" id="chkNewCustomer" name="chkNewCustomer" value="customer">
                                        <label style="display: inline-flex;" for="chkNewCustomer">( In order to save new customer information, kindly checked on checkbox )</label>
                                    </div>
                                </div>

                                <div id="pnlSearch">
                                    <div class="controls-row">
                                        <div class="control-group span3">
                                            <label class="label-bold">Contact No </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtSearchContact" runat="server" placeHolder="Contact No" CssClass="span12 txtSearchContact integers" MaxLength="12" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="control-group span3">
                                            <label class="label-bold">Email </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtSearchEmail" runat="server" placeHolder="Email" CssClass="span12 txtSearchEmail" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="control-group span3">
                                            <div>
                                                <br />
                                                <asp:Button runat="server" ID="btnSearch" Text="Search" class="btn btn-danger btnSearch"></asp:Button>
                                                <asp:Button runat="server" ID="btnCancelSearch" Text="Cancel" class="btn btn-warning btnCancelSearch"></asp:Button>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="content-widgets light-gray">
                            <div class="widget-head bluePSW">
                                <h3>Customer Information</h3>
                            </div>
                            <div class="widget-container">
                                <div id="pnlCustomer">
                                    <div class="controls-row">
                                        <div class="control-group span3">
                                            <label class="label-bold">Name</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtName" runat="server" placeHolder="Name" CssClass="span12 txtName TextBoxValidate" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="control-group span3">
                                            <label class="label-bold">Contact No </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtContact" runat="server" placeHolder="Contact No" CssClass="span12 txtContact TextBoxValidate" MaxLength="12" autocomplete="off" onkeypress="return IsAlphaNumeric(event);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="control-group span3">
                                            <label class="label-bold">Email</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtEmail" runat="server" placeHolder="Email" CssClass="span12 txtEmail TextBoxValidate" autocomplete="off" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="control-group span3">
                                            <label class="label-bold">Alternative Number</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtAlternativeNumber" runat="server" placeHolder="Alternative Number" CssClass="span12 txtAlternativeNumber TextBoxValidate" autocomplete="off" MaxLength="12" onkeypress="return IsAlphaNumeric(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row">
                                        <div class="control-group span3">
                                            <label class="label-bold">City</label>
                                            <div class="controls">
                                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="span12 ddlCity DropDownValidate"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="controls-row">
                                        <div class="control-group span12">
                                            <label class="label-bold">Address</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Style="height: 82px; resize: none;" runat="server" CssClass="span12 txtAddress" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="content-widgets light-gray">
                            <div class="widget-head bluePSW">
                                <h3>Ticket Information</h3>
                            </div>
                            <div class="widget-container">
                                <div class="controls-row">
                                    <%-- <div class="control-group span3">
                                        <label class="label-bold">Initiator </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlInitiator" CssClass="span12 ddlInitiator DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="control-group span3">
                                        <label class="label-bold">Department </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="span12 ddlDepartment DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group span3">
                                        <label class="label-bold">
                                            Assignee To
                                        <asp:Label ID="Label1" runat="server" Text="(Optional)" ForeColor="Red"></asp:Label>
                                        </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlAssignee" CssClass="span12 ddlAssignee"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group span3">
                                        <label class="label-bold">Priority  </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlPriority" CssClass="span12 ddlPriority DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="control-group span3">
                                        <label class="label-bold">Method Of Contact </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlRequestMode" CssClass="span12 ddlRequestMode DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="controls-row">


                                    <div class="control-group span3">
                                        <label class="label-bold">Request Type </label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="span12 ddlRequestType DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="control-group span3">
                                        <label class="label-bold">Category</label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="span12 ddlCategory DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="control-group span3">
                                        <label class="label-bold">Subcategory</label>
                                        <div class="controls">
                                            <asp:DropDownList runat="server" ID="ddlSubcategory" CssClass="span12 ddlSubcategory DropDownValidate"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                                <div class="controls-row dynamicFields" id="dynamicFields">
                                </div>

                                <div class="controls div_Attachment">
                                    <table><tr>
                                    <td>VIEW ATTACHMENT :</td> 
                                        <td><asp:HyperLink ID="hrefImg" runat="server" CssClass="span12 FileImageRef" Target="_new"><img src='/Images/Attachment.png' style='height: 30px; width: 30px;margin-right: 20px;' /></asp:HyperLink></td>
                                    
                                        </tr></table>
                                </div>

                                <div class="controls-row">


                                    <div class="controls-row">
                                        <div class="control-group span12">
                                            <label class="label-bold">Title </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtTitle" runat="server" placeHolder="Title" CssClass="span12 TextBoxValidate txtTitle"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row">
                                        <div class="control-group span12">
                                            <label class="label-bold">Initial findings </label>
                                            <div class="controls div_TextDescription">
                                                <cc1:Editor ID="txtDescription" Style="border: 1px solid; border-color: #d4d4d4" runat="server" CssClass="txtDescription" targetcontrolid="txtDescription"></cc1:Editor>
                                                <%--RichBoxValidate--%>
                                            </div>

                                            <div style="min-height: 100px; max-height: 200px; overflow-y: scroll; display: none" class="chatbaloon div_Description">
                                                <asp:Label runat="server" ID="Description" Style="white-space: pre-wrap;" CssClass="Description" Width="98.5%" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row Div_To">
                                        <div class="control-group span12">
                                            <label class="label-bold">
                                                Email To
                                        <asp:Label ID="lblmessage" runat="server" Text="(Note : Enter semicolon separated email address without spaces)" ForeColor="Red"></asp:Label>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox runat="server" ID="txtTo" placeholder="Email To" CssClass="span11 txtToEmail TextBoxValidate"></asp:TextBox>
                                                <%-- <div class="pull-right btn_AddToDiv">
                                            <asp:Button runat="server" ID="btn_AddTo" Text="Add" class=" myModalToEmail btn btn-info btn_AddTo" data-toggle="modal" data-target="#myModalToEmail"></asp:Button>
                                            <asp:Button runat="server" ID="btn_AddToClear" Text="Clear" class="btn btn-warning btn_AddToClear"></asp:Button>
                                        </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row Div_CC">
                                        <div class="control-group span12">
                                            <label class="label-bold">
                                                Email CC 
                                        <asp:Label ID="Label3" runat="server" Text="(Note : Enter semicolon separated email address without spaces)" ForeColor="Red"></asp:Label>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox runat="server" ID="txtCC" Text="support@psw.gov.pk;" placeholder="Email CC" CssClass="span11 txtCCEmail"></asp:TextBox>
                                                <div class="pull-right btn_AddCCDiv">
                                                    <asp:Button runat="server" ID="btn_AddCC" Text="Add" class="myModal btn btn-info btn_AddCC" data-toggle="modal" data-target="#myModal"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_AddCCClear" Text="Clear" class="btn btn-warning btn_AddCCClear"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row">
                                        <div class="control-group span12">
                                            <label class="label-bold">
                                                Attachment
                                            </label>
                                            <div class="controls">
                                                <input type="file" class="form-control Attachement" multiple="multiple" />
                                                <input type="button" id="Upload_New" class="btn btn-danger Upload_New Btn_UploadDiv_New" value="Upload" />
                                                <input type="button" id="Upload" class="btn btn-danger Upload Btn_UploadDiv" value="Upload" />
                                                <br />
                                                <strong>
                                                    <a href="#" class="Clearfileupload" style="color: blue">Clear Attachment</a>
                                                </strong>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="controls-row RptAttachment">
                                        <div class="widget-head bluePSW">
                                            <h3>Attachments</h3>
                                        </div>
                                        <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                            <thead>
                                                <tr>
                                                    <th style="width: 60%">File Name</th>
                                                    <th style="width: 20%">File Type</th>
                                                    <th>View</th>
                                                    <th>Download</th>
                                                    <th>Delete</th>
                                                </tr>
                                            </thead>
                                            <tbody class="wfattachment">
                                            </tbody>
                                        </table>
                                    </div>


                                    <div class="controls-row btn-danger" id="divError_" runat="server" visible="false">
                                        <div id="lblError_" runat="server"></div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="controls-row Div_ButttonSave">
                                        <div class="pull-right">
                                            <asp:Button runat="server" ID="btn_Save" Text="Save" ValidationGroup="ValidateOnSave" class="btn btn-danger btn_Save btn_Save_Hide"></asp:Button>
                                            <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" PostBackUrl="/Pages/Tickets.aspx" class="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                    <br />

                                </div>
                            </div>

                        </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="myModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="width: 690px !important">
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
                                            <h3>Add CC</h3>
                                        </div>
                                        <div class="widget-container">
                                            <uc1:MultipleSelectionBox runat="server" ID="MultipleSelectionBox" DestinationHeadingText="Selected Employee(s)" SourceHeadingText="Employee(s)" />
                                            <div class="controls-row" style="margin-bottom: 20px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_AddCC_" Text="Add" class="btn btn-danger btn_AddCC_"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_Close" Text="Cancel" class="btn btn-warning btn_Close"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="myModalToEmail" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="width: 690px !important">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">

                    <div class="modal-body">
                        <div class="row-fluid">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <input type="hidden" id="Hidden1" runat="server" class="hfId" />
                                    <div class="content-widgets light-gray">
                                        <div class="widget-head bluePSW">
                                            <h3>Add To</h3>
                                        </div>
                                        <div class="widget-container">

                                            <div class="controls-row">
                                                <div class="control-group span4">
                                                    <label class="label-bold">
                                                        Customer(s)
                                                    </label>
                                                    <div class="controls">
                                                        <select class="ListBoxCustomerSource" style="height: 200px;" multiple="multiple"></select>
                                                    </div>
                                                </div>


                                                <div class="control-group span4" style="text-align: center; margin-top: 80px;">
                                                    <div class="controls">
                                                        <asp:Button runat="server" ID="btnCustomerMoveRight" CssClass="btn btn-info btnCustomerMoveRight" OnClientClick="OnbtnCustomerMoveRight();" Text="  Move >>"></asp:Button>
                                                        <br />
                                                        <asp:Button runat="server" ID="btnCustomerMoveLeft" CssClass="btn btn-info" OnClientClick="OnbtnCustomerMoveLeft();" Style="margin-top: 20px;" Text="  << Move"></asp:Button>
                                                    </div>
                                                </div>


                                                <div class="control-group span4" style="margin-left: -22px">
                                                    <label class="label-bold">
                                                        Selected Customer(s)
                                                    </label>
                                                    <div class="controls">
                                                        <select class="ListBoxCustomerDestination" style="height: 200px;" multiple="multiple"></select>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="controls-row" style="margin-bottom: 20px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_AddTo_" Text="Add" class="btn btn-danger btn_AddTo_"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_Close_To" Text="Cancel" class="btn btn-warning btn_Close_To"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                </div>

            </div>
        </div>

        <div id="SearchPopupModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="width: 1000px !important; left: 33% !important; top: 5% !important">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row-fluid">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="content-widgets light-gray">
                                        <div class="widget-head bluePSW">
                                            <h3>Search CDMS Links</h3>
                                        </div>
                                        <div class="widget-container">

                                            <div class="controls-row">

                                                <div class="control-group span2">
                                                    <label class="label-bold">Product </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlSearchProduct" CssClass="span12 ddlSearchProduct"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Region </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlSearchRegion" CssClass="span12 ddlSearchRegion"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">City </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlSearchCity" CssClass="span12 ddlSearchCity"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Exchange/POP </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlSearchExchangePOP" CssClass="span12 ddlSearchExchangePOP"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Status </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddSearchlStatus" CssClass="span12 ddSearchlStatus"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Circuit Type </label>
                                                    <div class="controls">
                                                        <asp:DropDownList runat="server" ID="ddlSearchCircuitType" CssClass="span12 ddlSearchCircuitType"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="controls-row">
                                                <div class="control-group span2">
                                                    <label class="label-bold">Customer </label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSearchCustomer" placeholder="Customer" CssClass="span12  txtSearchCustomer"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Address </label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSearchAddress" placeholder="Address" CssClass="span12  txtSearchAddress"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">Branch Code </label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSearchBranchCode" placeholder="Branch Code" CssClass="span12  txtSearchBranchCode"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">CAM # </label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSearchCAM" placeholder="CAM #" CssClass="span12  txtSearchCAM"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <label class="label-bold">IP Address </label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSearchIPAddress" placeholder="IP Address" CssClass="span12  txtSearchIPAddress"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group span2">
                                                    <div class="pull-right">
                                                        <br />
                                                        <asp:Button runat="server" ID="btn_SearchCDMSLink" Text="Search" class="btn btn-danger btn_SearchCDMSLink"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row-fluid" style="margin-top: 5px; overflow-x: scroll; height: 300px; overflow-y: scroll">
                                                <div class="content-widgets light-gray">
                                                    <div class="widget-container">
                                                        <table class="paper-table responsive table table-paper table-striped table-sortable table-bordered RptTable">
                                                            <thead>
                                                                <tr>
                                                                    <th>Add Detail </th>
                                                                    <th>Product</th>
                                                                    <th>Region</th>
                                                                    <th>City</th>
                                                                    <th>Customer</th>
                                                                    <th>Address</th>
                                                                    <th>Branch Code</th>
                                                                    <th>Exchange/POP</th>
                                                                    <th>CAM #</th>
                                                                    <th>IP Address</th>
                                                                    <th>Secondary IP Address</th>
                                                                    <th>Status</th>
                                                                    <th>Circuit Type </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody class="wfCDMSLinks">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="controls-row" style="margin-bottom: 5px; margin-top: 5px;">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" ID="btn_Cancel_SearchPopupModal" Text="Cancel" class="btn btn-warning btn_Cancel_SearchPopupModal"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script type="text/x-jQuery-tmpl" id="wfForm">

        <tr class="tr_">
            <td>
                <input type="text" class="POCName" value="${POCName}" /></td>
            <td>
                <input type="text" class="POCContact" value="${POCContact}" /></td>
            <td>
                <input id="txtPOCEmail" type="email" class="POCEmail" value="${POCEmail}" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="wfattachment">
        <tr>
            <td>
                <asp:Label runat="server" ID="lbl" Text="${FileOriginalName}"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label runat="server" ID="Label4" Text="${Filetype}"></asp:Label>
            </td>

            <td style="text-align: center">
                <a target="_blank" onclick="var originalTarget = document.forms[0].target; document.forms[0].target = '_blank'; setTimeout(function () { document.forms[0].target = originalTarget; }, 3000);" href="${FilePath}">
                    <img src="/Images/book-open-icon.png" />
                </a>
            </td>
            <td style="text-align: center">
                <a target="_blank" href="${FilePath}" download>
                    <img src="/images/downld.gif" />
                </a>
            </td>
            <td style="text-align: center">
                <asp:ImageButton runat="server" OnClientClick="ImageDelete(${FileId})" ImageUrl="/images/deletefile.gif" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="wfattachment_New">
        <tr>
            <td>
                <asp:Label runat="server" ID="Label2" Text="${FileOriginalName}"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label runat="server" ID="Label5" Text="${Filetype}"></asp:Label>
            </td>

        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="wfCDMSLinks">
        <tr class="tr">
            <td style="text-align: center">
                <input type="image" class="buttonTicket" src="../Images/select.png" height="30" />
            </td>
            <td>${Product}
                <input class="HfRunningTicketCountAgainstLink" type="hidden" value='${RunningTicketCountAgainstLink}' />
                <input class="HfProductId" type="hidden" value='${ProductId}' />
                <input class="HfManageSevicesMasterId" type="hidden" value='${ManageSevicesMasterId}' />
            </td>
            <td>${Region}</td>
            <td>${City}</td>
            <td>${Client}</td>
            <td>${Address}</td>
            <td>${Branch_Code}</td>
            <td>${Exchange_POP}</td>
            <td>${CAM}</td>
            <td>${IP}</td>
            <td>${Secondary_IP}</td>
            <td>${ProductStatus}</td>
            <td>${CircuitType}</td>

        </tr>
    </script>

    <script type="text/javascript">


        //$(document).ready(function () {
        //    setTimeout('$("#container").css("opacity", 1)', 1000);
        //});


        function pageLoad() {
            //$('.txtToEmail').tagator();
            //$('.txtCCEmail').tagator();

            $(".Clearfileupload").click(function () {
                $('.Attachement').val('');
                return false;
            });

            $('.txtToEmail').on('keypress', function (e) {
                if (e.which == 32)
                    return false;
            });

            $('.txtCCEmail').on('keypress', function (e) {
                if (e.which == 32)
                    return false;
            });

            OnLoad();



            $(".btn_Save").click(function () {
                PerformAction();
            });

            $(".Upload").click(function () {
                UploadImage();
            });

            $(".Upload_New").click(function () {
                UploadImage();
            });

            $(".btnSearchPopupModal").click(function () {
                $('.ddlSearchProduct').val('0');
                $('.ddlSearchCircuitType').val('0');
                $('.ddSearchlStatus').val('0');
                $('.ddlSearchRegion').val('0').change();
                $('.txtSearchCustomer').val('');
                $('.txtSearchAddress').val('');
                $('.txtSearchCAM').val('');
                $('.txtSearchIPAddress').val('');
                $('.txtSearchBranchCode').val('');
                $('.wfCDMSLinks').html('');
                $('#SearchPopupModal').zIndex(1050);
            });

            $(".btn_SearchCDMSLink").click(function () {
                SearchCDMSLink();
            });

            $(".btn_Cancel_SearchPopupModal").click(function () {
                CloseSearchPopupModal();
            });

            $(".btn_AddCC").click(function () {
                $('#myModal').zIndex(1050);
            });
            $(".btn_Close").click(function () {
                ClosePopup();
            });
            $(".btn_AddCC_").click(function () {
                ADDCC();
                ClosePopup();
            });
            $(".btn_AddCCClear").click(function () {
                $('.txtCCEmail').val("");
            });

            $(".btn_AddTo").click(function () {
                $('#myModalToEmail').zIndex(1050);
            });
            $(".btn_Close_To").click(function () {
                ClosePopupTo();
            });
            $(".btn_AddTo_").click(function () {
                ADDTo();
                ClosePopupTo();
            });
            $(".btn_AddToClear").click(function () {
                $('.txtToEmail').val("");
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


        function CloseSearchPopupModal() {
            $('#SearchPopupModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }


        function ImageDelete(id) {
            DeleteAtachment(id);
        }

        function ClosePopup() {
            ClearAddCC();
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function OpenPopup() {
            $('.btn_AddCC').click();
            $('#myModal').zIndex(1050);
        }




        function ClosePopupTo() {
            //  ClearAddCC();
            $('#myModalToEmail').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }


        function OpenPopup() {
            $('.btn_AddTo').click();
            $('#myModalToEmail').zIndex(1050);
        }

        function ControlsValidateFunction() {

            var State = true;
            $(".DropDownValidate").each(function () {
                if ($(this).val() == null) {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else if ($(this).val() == "0") {
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
            });
            $(".RichBoxValidate").each(function () {
                var InnerText = $find('ContentPlaceHolder1_txtDescription_ctl02').get_content();
                if (InnerText == "") {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border", "1px solid");
                    $(this).css("border-color", "#d4d4d4");
                }
            });
            //$(".txtToEmail").each(function () {
            //    var emailtextboxvalue = $('.txtToEmail').val();
            //    if (emailtextboxvalue == "") {
            //        $(this).closest(".controls").find(".tagator").css("border-color", "red");
            //        State = false;
            //    }
            //    else {
            //        $(this).closest(".controls").find(".tagator").css("border-color", "#abadb3");
            //    }
            //});

            if (State == true) {
                var emailtextboxvalue = $('.txtToEmail').val();
                var result = validateMultipleEmailsCommaSeparated(emailtextboxvalue, ';');
                if (result) {
                }
                else {
                    State = false;
                    AlertBox("Alert", "Invalid To Email Address", "warning");
                }
            }

            if (State == true) {
                var emailtextboxvalue = $('.txtCCEmail').val();
                var result = validateMultipleEmailsCommaSeparated(emailtextboxvalue, ';');
                if (result) {
                }
                else {
                    State = false;
                    AlertBox("Alert", "Invalid CC Email Address", "warning");
                }
            }


            $(".hf_Validate").val(State);

            return State;
        }


        //function validateEmail(field) {
        //    var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
        //    return (regex.test(field)) ? true : false;
        //}

        function validateMultipleEmailsCommaSeparated(emailcntl, seperator) {

            var value = emailcntl;
            if (value != '') {
                var result = value.split(seperator);
                for (var i = 0; i < result.length; i++) {
                    if (result[i] != '') {
                        if (!validateEmail(result[i])) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        function ValidatingEmail(source, args) {
            //debugger;
            var emailtextboxvalue = $('.txtTo').val();
            var result = validateMultipleEmailsCommaSeparated(emailtextboxvalue, ';');
            if (result) {
                args.IsValid = true;
                return;
            }
            else {
                args.IsValid = false;
                return;
            }
        }


        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode || e.which;

            //Regex to allow only Alphabets Numbers Dash Underscore and Space
            var pattern = /^[a-z\d\-_\s]+$/i;

            //Validating the textBox value against our regex pattern.
            var isValid = pattern.test(String.fromCharCode(keyCode));
            return isValid;
        }


    </script>


</asp:Content>

