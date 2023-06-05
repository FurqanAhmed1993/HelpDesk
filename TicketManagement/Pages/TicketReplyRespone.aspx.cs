using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_TicketReplyRespone : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Text = "Reply";
        if (!IsPostBack)
        {
            hf_UserId.Value = Convert.ToString(UserId);
            hf_ChatTypeId.Value = (Request.QueryString["ChatTypeId"]);
            hf_MasterId.Value = (Request.QueryString["TMID"]);
            hfAssigneeId.Value = (Request.QueryString["AssigneeId"]);
            hfInitiatorId.Value = (Request.QueryString["InitiatorId"]);

            int ChatTypeId = Convert.ToInt32(hf_ChatTypeId.Value);
            if (ChatTypeId == (int)Constant.ChatType.Ticket_Internel_Chat)
            {
                lnkbtnTicketDetail.Visible = false;
                lblHeader.Text = "Internal Chat History";
            }
            else if (ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response)
            {
                lblHeader.Text = "Ticket Reply / Response History";
            }
            else if (ChatTypeId == (int)Constant.ChatType.Task_Reply_Response)
            {
                lnkbtnTicketDetail.Visible = false;
                lblHeader.Text = "Task Reply / Response History";
            }
        }
    }
}