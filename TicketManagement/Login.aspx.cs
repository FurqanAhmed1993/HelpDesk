using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Login : Base
{

    TicketSystemEntities context = new TicketSystemEntities();

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblYear.Text = Convert.ToString(DateTime.Now.Year);
        }
    }

    protected void login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (IsValid)
        {
            try
            {
                lblErrorNew.Text = "";
                divError.Visible = false;
                string ErrorMessage = string.Empty;
                string password = login1.Password.Trim();
                string loginId = login1.UserName.Trim();
                UserLogin user = context.UserLogins.FirstOrDefault(u => u.Login_ID == loginId && u.Password == password && u.Is_Active == true);
                if (user != null)
                {
                    int UserId = user.User_Code;
                    if (user.IsEnabled == true)
                    {
                        Common.ResetBaseClass();
                        int RoleId = Convert.ToInt32(user.Role_Code);
                        Base baseClass = new Base();
                        baseClass.EmployeeId = user.Setup_Employee2.Emp_Id;
                        baseClass.UserId = user.User_Code;
                        baseClass.EmailAddress = user.Setup_Employee2.EmailId;
                        baseClass.FullName = user.Full_Name;
                        baseClass.RoleCode = Convert.ToString(RoleId);
                        baseClass.IsSuperAdmin = RoleId == (int)Constant.Role.SuperAdmin ? true : false;
                        baseClass.IsAdmin = RoleId == (int)Constant.Role.Admin ? true : false;
                        baseClass.IsLevel_1 = RoleId == (int)Constant.Role.Level_1 ? true : false;
                        //baseClass.IsEmployee = RoleId == (int)Constant.Role.Employee ? true : false;
                        //baseClass.IsIncharge = RoleId == (int)Constant.Role.Incharge ? true : false;
                        //baseClass.IsClient = RoleId == (int)Constant.Role.Client ? true : false;
                        int? DesignationId_ = user.Setup_Employee != null ? (user.Setup_Employee2.DesignationId == null ? 0 : Convert.ToInt32(user.Setup_Employee2.DesignationId)) : 0;
                        baseClass.DesignationId = Convert.ToInt32(DesignationId_);
                        int? DepartmentId_ = user.Setup_Employee != null ? Convert.ToInt32(user.Setup_Employee2.DepartmentId == null ? 0 : user.Setup_Employee2.DepartmentId) : 0;
                        baseClass.DepartmentId = Convert.ToInt32(DepartmentId_);

                        InsertLoginHistory(UserId, true);
                        e.Authenticated = true;
                        string DefaultPage = "/Default.aspx";
                        Response.Redirect(DefaultPage, true);
                    }
                    else
                    {
                        e.Authenticated = false;
                        InsertLoginHistory(UserId, false);
                        divform.Attributes.Add("class", "has-error");
                        lblErrorNew.Text = "Disabled user";
                        divError.Visible = true;
                    }
                }
                else
                {
                    e.Authenticated = false;
                    divform.Attributes.Add("class", "has-error");
                    lblErrorNew.Text = "The Login Id or Password is incorrect. ";
                    divError.Visible = true;
                }
                context = null;
            }
            catch (Exception ex)
            {
                e.Authenticated = false;
            }
        }
    }

    public void InsertLoginHistory(int UserId_, bool IsSuccess)
    {
        UserLoginHistory obj = new UserLoginHistory();
        obj.UserId = UserId_;
        obj.ApplicationId = (int)Constant.Application.Ticket;
        obj.IsSuccess = IsSuccess;
        obj.UserIP = UserIP;
        obj.IsActive = true;
        obj.CreatedBy = UserId_;
        obj.CreatedDate = DateTime.Now;
        context.UserLoginHistories.Add(obj);
        context.SaveChanges();
    }

    #endregion

}