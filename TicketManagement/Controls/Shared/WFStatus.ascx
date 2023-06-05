<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFStatus.ascx.cs" Inherits="Controls_WF_WFStatus" %>
<%@ Register Src="~/Controls/Shared/StatusImage.ascx" TagPrefix="uc2" TagName="StatusImage" %>


<div runat="server" id="divWrapper">


    <div id="divStatusImageWrapper">
        <uc2:StatusImage ID="StatusImage1" runat="server" StatusType="UNKNOWN" ViewStatus="True" />
        <span runat="server" id="spnViewStatus" visible="false" style="font-size: 10px; padding-right: 5px; padding-left: 0px;">View Status</span>
    </div>


    <div id="divStatusPanel" class="WFStatusPanel hidden">
        <div style="overflow-y: scroll; max-height: 300px;">

            <asp:GridView ID="grdWFSteps" runat="server" AutoGenerateColumns="False" Font-Bold="false"
                CssClass="paper-table responsive table table-paper table-striped table-sortable table-bordered" GridLines="None" Font-Names="calbri" Font-Size="Small" EnableModelValidation="True"
                OnDataBinding="GridView1_DataBinding" OnDataBound="GridView1_DataBound" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle CssClass="th_rowWF"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Step">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStep" EnableTheming="false" Text="<%# Container.DataItemIndex + 1 %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Assignee" HeaderText="Action by" ItemStyle-Width="15%" ItemStyle-Wrap="true" />
                    <asp:TemplateField HeaderText="Findings">
                        <ItemTemplate>
                            <div style="white-space: pre-line;">
                                <%# Eval("Description") %>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" Width="50%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ItemStyle-Width="10%" ItemStyle-Wrap="true" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>' />
                            <br />
                            <uc2:StatusImage ID="StatusImage2" runat="server" Status='<%# Eval("Status") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" Width="10%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="hidden" />
            </asp:GridView>
        </div>

        <table id="tblExtraText" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
            visible="false">
            <tr>
                <td>
                    <asp:Label ID="lblExtraText" runat="server" Text="" EnableTheming="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</div>


