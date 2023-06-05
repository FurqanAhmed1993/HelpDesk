using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Base obj = new Base();
            lblLoginUser.Text = obj.FullName;
            lblYear.Text = Convert.ToString(DateTime.Now.Year);
            //if (Convert.ToInt32(obj.RoleCode) == (int)Constant.Role.Client)
            //{
            //    hf_IsCustomer.Value = "1";
            //}
        }
     
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        new Base().ExpireCookie();
        Common.ResetBaseClass();
        Response.Redirect("~/Login.aspx");
    }
}
