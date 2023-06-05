using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_TaskDetail : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                hf_IsInitiator.Value = Request.QueryString["IsInitiator"];
                int TMID = Convert.ToInt32(Request.QueryString["TMID"]);
                hf_TicketMasterId.Value = TMID.ToString();
                int TaskMasterId = Convert.ToInt32(Request.QueryString["hfTaskMasterId"]);
                hfTaskMasterId.Value = TaskMasterId.ToString();
                WFStatus.MasterId = TaskMasterId;
                hf_UserId.Value = Convert.ToString(base.UserId);
                hf_DepartmentId.Value = Convert.ToString(base.DepartmentId);
                hf_IsSuperAdmin.Value = (base.IsSuperAdmin == true ? "true" : "false");
                hf_IsAdmin.Value = (base.IsAdmin == true ? "true" : "false");
                hf_IsIncharge.Value = (base.IsIncharge == true ? "true" : "false");


            }
        }
        catch
        {
        }
    }
}