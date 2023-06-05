<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingAndSorting.ascx.cs"
    Inherits="ERP.Website.controls.PagingAndSorting" %>

<div class="widget-container">
    <div class="controls-row">
        <%-- <div class="control-group span3">
            <div class="controls">
                Page Size: <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" CssClass="span12 ddlPrioritySearch"></asp:DropDownList>
            </div>
        </div>

        <div class="control-group span3">
            <div class="controls">
                <asp:ImageButton ID="imgPrevious" runat="server" OnClick="imgPrevious_Click"
                    ImageUrl="~/Images/previous_btn.jpg" ToolTip="Previous" Width="20px" Height="29px" />&nbsp;
             <asp:DropDownList CssClass="span12" ID="ddlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;
             <asp:ImageButton ID="imgNext" runat="server" OnClick="imgNext_Click" ImageUrl="~/Images/next_btn.jpg" ToolTip="Next" Width="20px" Height="29px" />
                <asp:Label ID="lblRecordCountText" runat="server" Text="Total Records : "></asp:Label>
                <asp:Label ID="lblRecordCount" CssClass="clsRecordCount" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
            </div>
        </div>--%>



        <table cellpadding="0" cellspacing="0" runat="server" id="tblPaging">
            <tr>
                <td nowrap="nowrap" style="vertical-align: top">&nbsp;&nbsp;
            <asp:Label ID="lblPageSize" runat="server" Text="Page Size:" Font-Bold="true"></asp:Label>&nbsp;
                        <asp:DropDownList CssClass="DropDown_" ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td style="vertical-align: top">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="vertical-align: top">
                                <asp:ImageButton ID="imgPrevious" runat="server" OnClick="imgPrevious_Click"
                                    ImageUrl="~/Images/previous_btn.jpg" ToolTip="Previous" Width="20px" Height="29px" />&nbsp;
                            </td>
                            <td style="vertical-align: top">
                                <asp:DropDownList CssClass="DropDown_" Height="29px" ID="ddlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                            </td>
                            <td style="vertical-align: top">
                                <asp:ImageButton ID="imgNext" runat="server" OnClick="imgNext_Click" ImageUrl="~/Images/next_btn.jpg" ToolTip="Next" Width="20px" Height="29px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td nowrap="nowrap" style="vertical-align: middle">&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblRecordCountText" runat="server" Text="Total Records : "></asp:Label>
                    <asp:Label ID="lblRecordCount" CssClass="clsRecordCount" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                </td>
            </tr>
        </table>

    </div>
</div>
