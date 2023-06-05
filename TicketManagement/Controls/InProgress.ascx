<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InProgress.ascx.cs" Inherits="Controls_InProgress" %>


<style type="text/css">
    .UpdateProgressTemplateContent, .MessageWrapper.ShowAsModalPopup .divMessage {
        border: 1px solid gray;
        position: fixed;
        left: 50%;
        top: 50%;
        margin-left: -250px;
        margin-top: -37px;
        width: 500px;
        height: 74px;
        background-color: white;
        border-radius: 5px;
        padding: 10px;
        z-index: 1000;
    }

    .UpdateProgressTemplateContent {
        text-align: center;
        margin-left: -37px;
        margin-top: -37px;
        width: 74px;
        height: 74px;
    }


        .UpdateProgressTemplateContent.WithMessage {
            margin-left: -250px;
            width: 500px;
        }


            .UpdateProgressTemplateContent.WithMessage .Message {
                padding-right: 20px;
            }

            .UpdateProgressTemplateContent.WithMessage .Image {
                width: 30px;
            }

    .UpdateProgressTemplate, .MessageWrapper.ShowAsModalPopup .MessageFullWindowBackground {
        position: fixed;
        width: 100%;
        height: 100%;
        left: 0;
        top: 0;
        background-color: Gray;
        opacity: 0.7;
        filter: alpha(opacity = 70);
        z-index: 900;
    }
</style>
<div class="UpdateProgressTemplate"></div>
<table runat="server" id="divContent" class="UpdateProgressTemplateContent">
    <tr>

        <td runat="server" id="tdMessage" class="Message">
            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="Please wait while processing..."></asp:Label>
            <br />
            <asp:Image ID="Image1" Height="30px" runat="server" alt="" ImageUrl="~/Images/ajax-loader.gif" />
        </td>
        <%-- 
        <td class="Image">
           
        </td>--%>
    </tr>
</table>
