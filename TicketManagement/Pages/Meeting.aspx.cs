using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Meeting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hf_TicketMasterId.Value = (Request.QueryString["TMID"]);
            hf_TicketMeetingId.Value = (Request.QueryString["hfTicketMeetingId"]);
            hf_View.Value = (Request.QueryString["IsView"]);
        }
    }
}