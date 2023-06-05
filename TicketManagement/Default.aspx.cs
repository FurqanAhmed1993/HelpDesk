using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                //string Wallboard_Refresh_TimeInSec_ = System.Configuration.ConfigurationManager.AppSettings["Wallboard_Refresh_TimeInSec"];
                //Wallboard_Refresh_TimeInSec.Value = Wallboard_Refresh_TimeInSec_.Trim() == "" ? "5" : (Wallboard_Refresh_TimeInSec_.Trim() == "0" ? "5" : Wallboard_Refresh_TimeInSec_.Trim());
                //Hf_LoginUserName.Value = FullName.ToString();
                //string Dashboard_Refresh_TimeInSec_ = System.Configuration.ConfigurationManager.AppSettings["Dashboard_Refresh_TimeInSec"];
                //Dashboard_Refresh_TimeInSec.Value = Dashboard_Refresh_TimeInSec_.Trim() == "" ? "3" : (Dashboard_Refresh_TimeInSec_.Trim() == "0" ? "3" : Dashboard_Refresh_TimeInSec_.Trim());
                //Hf_LoginUserName.Value = FullName.ToString();
                //hf_UserLoginId.Value = UserId.ToString();
                //if (Convert.ToInt32(RoleCode) == (int)Constant.Role.Client)
                //{
                //    hf_IsCustomer.Value = "1";
                //}
            }
            catch { }
        }
    }
}