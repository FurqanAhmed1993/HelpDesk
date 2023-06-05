using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


public partial class Pages_ChangePassword : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentPassword.Text = "";
        }
    }

    protected void btn_ChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            string old_Password = CurrentPassword.Text.Trim();
            string New_Password = NewPassword.Text.Trim();
            int UserId_ = new Base().UserId;
            string Result = string.Empty;
            ChangePassword(UserId_, old_Password, New_Password);


        }
        catch (Exception ex)
        {
            string mesg = ex.Message != null ? ex.Message : "Old Password is Wrong ";
            Error(mesg);
        }
    }

    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void ChangePassword(int UId, string OldPassword, string NewPassword)
    {
        var list = context.UserLogins.FirstOrDefault(a => a.User_Code == UId && a.Password == OldPassword && a.Is_Active == true);
        if (list != null)
        {
            if (OldPassword == NewPassword)
            {
                Error("Already used password. Please try again.");
            }
            else
            {
                list.Modified_By = UId;
                list.Modified_Date = DateTime.Now;
                list.Password = NewPassword;
                context.SaveChanges();
                Success("Password Changed Successfully");
            }
        }
        else
        {
            Error("Invalid Old Password");
        }
    }
}