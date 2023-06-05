<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultipleSelectionBox.ascx.cs" Inherits="Controls_Shared_MultipleSelectionBox" %>
<script src="/js/Pages_JS/MultipleSelectionBox.js"></script>

<div class="row-fluid">
    <div class="widget-container">
        <div class="controls-row">
            <div class="control-group span5">
                <label class="control-label">Department</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="ddlDepartmentCC" CssClass="span12 ddlDepartmentCC"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="controls-row">
            <div class="control-group span4">
                <label class="label-bold">
                    <asp:Label Text="" ID="lblSource" runat="server"> </asp:Label>
                </label>
                <div class="controls">
                    <select class="ListBoxSource" style="height: 200px;" multiple="multiple"></select>
                </div>
            </div>


            <div class="control-group span4" style="text-align: center; margin-top: 80px;">
                <div class="controls">
                    <asp:Button runat="server" ID="btnMoveRight" CssClass="btn btn-info btnMoveRight" OnClientClick="OnClientClick_Add();" Text="  Move >>"></asp:Button>
                    <br />
                    <asp:Button runat="server" ID="btnMoveLeft" CssClass="btn btn-info" OnClientClick="OnClientClick_Remove();" Style="margin-top: 20px;" Text="  << Move"></asp:Button>
                </div>
            </div>


            <div class="control-group span4" style="margin-left: -22px">
                <label class="label-bold">
                    <asp:Label Text="" ID="lblDestination" runat="server"> </asp:Label>
                </label>
                <div class="controls">
                    <select class="ListBoxDestination" style="height: 200px;" multiple="multiple"></select>
                </div>
            </div>
        </div>
    </div>
</div>


<script type="text/javascript">
    $(document).ready(function () {
        OnLoadControl();
    });


</script>
