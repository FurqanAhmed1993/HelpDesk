using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Task : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hf_UserId.Value = Convert.ToString(UserId);
            hf_TicketMasterId.Value = (Request.QueryString["TMID"]);
            hfTaskMasterId.Value = (Request.QueryString["hfTaskMasterId"]);
            hf_View.Value = (Request.QueryString["IsView"]);
           
        }
    }
}