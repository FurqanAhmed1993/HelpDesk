using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wallboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string Wallboard_Refresh_Time = System.Configuration.ConfigurationManager.AppSettings["Wallboard_Refresh_TimeInSec"];
                Wallboard_Refresh_TimeInSec.Value = Wallboard_Refresh_Time.Trim() == "" ? "5" : (Wallboard_Refresh_Time.Trim() == "0" ? "5" : Wallboard_Refresh_Time.Trim());

              
            }
        }
        catch { }
    }
}