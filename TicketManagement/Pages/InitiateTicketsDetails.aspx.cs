using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InitiateTicketsDetails : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bool CheckTMID = Request.QueryString.ToString().Contains("TMID");
                if (CheckTMID)
                {
                    hfEmailProductId.Value = Convert.ToString((int)Constant.Product.Email);
                    hf_CloseTicketStatus.Value = Convert.ToString((int)Constant.TicketStatus.Closed);
                  
                    int TMID = Convert.ToInt32(Request.QueryString["TMID"]);
                    if (TMID > 0)
                    {
                        bool IsInitiator = Convert.ToBoolean(Request.QueryString["hf_IsInitiator"]);
                        hf_IsInitiator.Value = IsInitiator == true ? "1" : "0";
                        hf_TicketMasterId.Value = TMID.ToString();
                        hf_UserId.Value = Convert.ToString(base.UserId);
                        hf_DepartmentId.Value = Convert.ToString(base.DepartmentId);
                        hf_IsSuperAdmin.Value = (base.IsSuperAdmin == true ? "true" : "false");
                        hf_IsAdmin.Value = (base.IsAdmin == true ? "true" : "false");
                        hf_IsIncharge.Value = (base.IsIncharge == true ? "true" : "false");
                        hf_RoleCode.Value = RoleCode;
                        //WFStatus.MasterId = TMID;
                       
                    }
                }
            }
        }
        catch
        {
        }
    }

}