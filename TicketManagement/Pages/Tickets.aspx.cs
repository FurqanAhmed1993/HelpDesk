using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;

public partial class Pages_Tickets : Base
{

    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            //txtTicketDateFrom.Text = txtTicketDateTo.Text = Convert.ToString(DateTime.Now.ToString(Constant.DateFormat));
            HfIsAdmin.Value = Convert.ToString(IsAdmin == true ? "1" : "0");
            HfIsSuperAdmin.Value = Convert.ToString(IsSuperAdmin == true ? "1" : "0");
            Ticket_Reply_Response.Value = Convert.ToString((int)Constant.ChatType.Ticket_Reply_Response);
            Ticket_Internel_Chat.Value = Convert.ToString((int)Constant.ChatType.Ticket_Internel_Chat);
            hfCustomerId.Value = Convert.ToString(CustomerId);
            hfDepartmentId.Value = Convert.ToString(DepartmentId);
            hfUserId.Value = Convert.ToString(UserId);
            hfRoleId.Value = Convert.ToString(RoleCode);

           
        }
    }

}