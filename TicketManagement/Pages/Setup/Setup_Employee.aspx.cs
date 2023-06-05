using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;


public partial class Pages_Setup_Setup_Employee : Base
{
    TicketSystemEntities context = new TicketSystemEntities();
    int? NullInt = null;

    public bool Is_ClickOnEdit
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDefaultPassword.Text = Constant.DefaultPassword;
            Is_ClickOnEdit = false;
            HFRolId.Value = Convert.ToString((int)Constant.Role.Client);
            BindDropDown();
            BindRepeater();
        }
        PagingHandler();
    }

    #region PAGING
    private void PagingHandler()
    {
        PagingAndSorting.ImgNext.Click += ImgNext_Click;
        PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
        PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
        PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    }

    void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    #endregion

    public void BindDropDown()
    {

        var List_Department = context.Setup_Department.Where(a => a.IsActive == true).OrderBy(v => v.DepartmentName).ToList();
        Common.BindDropDown(ddlDepartment, List_Department, "DepartmentName", "DepartmentId", true, false);

        //var List_Designation = context.Setup_Designation.Where(a => a.IsActive == true).OrderBy(v => v.DesignationName).ToList();
        //Common.BindDropDown(ddlDesignation, List_Designation, "DesignationName", "DesignationId", true, false);

        if (!IsSuperAdmin)
        {
            int superAdminRole = (int)Constant.Role.SuperAdmin;
            var listRole = context.Roles.Where(a => a.Is_Active == true && a.ApplicationId == (int)Constant.Application.Ticket && a.Role_Code != superAdminRole
            ).OrderBy(v => v.Role_Name).ToList();
            Common.BindDropDown(ddlRole, listRole, "Role_Name", "Role_Code", true, false);
            Common.BindDropDown(ddlRole_Search, listRole, "Role_Name", "Role_Code", true, false);
        }
        else
        {
            var listRole = context.Roles.Where(a => a.Is_Active == true && a.ApplicationId == (int)Constant.Application.Ticket
           ).OrderBy(v => v.Role_Name).ToList();
            Common.BindDropDown(ddlRole, listRole, "Role_Name", "Role_Code", true, false);
            Common.BindDropDown(ddlRole_Search, listRole, "Role_Name", "Role_Code", true, false);
        }

    }
    private void BindRepeater()
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
            int pageSize = 50;
            int pageNumber = 1;
            if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
            {
                pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
            }
            if (PagingAndSorting.DdlPage.Items.Count > 0)
            {
                pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
            }

            int skip = pageNumber * pageSize - pageSize;
            int Role_ = Convert.ToInt32(ddlRole_Search.SelectedValue == "" ? "0" : ddlRole_Search.SelectedValue);
            string Username = txtUsernameSearch.Text.ToString();
            string Loginid = txtLogininSearch.Text.ToString();

            if (!IsSuperAdmin)
            {
                int superAdminRole = (int)Constant.Role.SuperAdmin;
                var List = context.UserLogins.Where(a => a.Is_Active == true
                 && (a.Full_Name.Contains(Username) || Username == String.Empty)
                 && (a.Login_ID.Contains(Loginid) || Loginid == String.Empty)
                 && (a.Role_Code == Role_ || Role_ == 0)
                 && (a.Role_Code != superAdminRole)
                 )
                      .Select(c => new
                      {
                          UserCode = c.User_Code,
                          RoleId = c.Role_Code,
                          Role = c.Role.Role_Name,
                          EmployeeName = c.Setup_Employee2.EmployeeName,
                          EmailId = c.Login_ID,
                          EmployeeCode = c.Setup_Employee2.Emp_Id,
                          PhoneNo = c.Setup_Employee2.PhoneNo,
                          Department = c.Setup_Employee2.Setup_Department.DepartmentName,
                          Designation = c.Setup_Employee2.Setup_Designation.DesignationName,
                          IsEnable = c.IsEnabled == true ? "Enabled" : "Disabled",
                          CreatedDate = c.Created_Date,
                          Address = c.Setup_Employee2.Address,
                      }).OrderBy(a => a.CreatedDate).ToList();

                var List1 = List.OrderBy(a => a.CreatedDate).ThenBy(a => a.EmployeeName).Skip(skip).Take(pageSize).ToList();
                rpt.DataSource = List1;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(List.Count());
            }
            else
            {
                var List = context.UserLogins.Where(a => a.Is_Active == true
                && (a.Full_Name.Contains(Username) || Username == String.Empty)
                && (a.Login_ID.Contains(Loginid) || Loginid == String.Empty)
                && (a.Role_Code == Role_ || Role_ == 0)
                )
                     .Select(c => new
                     {
                         UserCode = c.User_Code,
                         RoleId = c.Role_Code,
                         Role = c.Role.Role_Name,
                         EmployeeName = c.Setup_Employee2.EmployeeName,
                         EmailId = c.Login_ID,
                         EmployeeCode = c.Setup_Employee2.Emp_Id,
                         PhoneNo = c.Setup_Employee2.PhoneNo,
                         Department = c.Setup_Employee2.Setup_Department.DepartmentName,
                         Designation = c.Setup_Employee2.Setup_Designation.DesignationName,
                         IsEnable = c.IsEnabled == true ? "Enabled" : "Disabled",
                         CreatedDate = c.Created_Date,
                         Address = c.Setup_Employee2.Address,
                     }).OrderBy(a => a.CreatedDate).ToList();

                var List1 = List.OrderBy(a => a.CreatedDate).ThenBy(a => a.EmployeeName).Skip(skip).Take(pageSize).ToList();
                rpt.DataSource = List1;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(List.Count());
            }

        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {

            //string Field = "";
            //Label lblRole = (Label)e.Item.FindControl("lblRole");
            //int _Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfRId")).Value);
            //List<Setup_UserRoleMapping> mapping = context.Setup_UserRoleMapping.Where(a => a.IsActive == true && a.UserLogin.Setup_Employee.Emp_Id == _Id && a.Setup_Application.ApplicationID == (int)Constant.Application.Ticket).ToList();
            //if (mapping != null && mapping.Count > 0)
            //{
            //    // Field = "<ul style='text-align: justify;margin-left: 11px; '>";
            //    for (int i = 0; i < mapping.Count; i++)
            //    {
            //        Field = mapping[i].Role.Role_Name;
            //        break;
            //        //string Role = mapping[i].Role.Role_Name;
            //        //Field = Field + "<li>";
            //        //Field = Field + Role + "</li>";
            //    }
            //}
            ////Field = Field + "</ul>";
            //lblRole.Text = Field;

            //if (!IsSuperAdmin)
            //{
            //    int RoleId_ = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfRoleId")).Value);
            //}


        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblError.InnerText = "";
        try
        {
            int RoleId = Convert.ToInt32(ddlRole.SelectedValue == "" ? "0" : ddlRole.SelectedValue);
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);


            using (TransactionScope scope = new TransactionScope())
            {
                if (Id == 0)
                {
                    string Msg = CheckNameIfExist(Id, txtEmailId.Text.Trim());
                    if (Msg == "")
                    {
                        Add();
                    }
                    else
                    {
                        Error(Msg);
                    }
                }
                else
                {
                    Update();
                }
                scope.Complete();
            }



        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = "Error : " + ex.Message;
        }
    }
    private void Add()
    {
        int RoleId = Convert.ToInt32(ddlRole.SelectedValue == "" ? "0" : ddlRole.SelectedValue);
        DateTime dt = DateTime.Now;
        Setup_Employee Obj = new Setup_Employee();
        Obj.EmployeeName = txtemloyeeName.Text.Trim();
        Obj.EmailId = txtEmailId.Text.Trim();
        Obj.PhoneNo = txtPhoneNo.Text.Trim();
        Obj.DepartmentId = (ddlDepartment.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlDepartment.SelectedValue));
        Obj.DesignationId = NullInt;
        Obj.Address = txtAddress.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        context.Setup_Employee.Add(Obj);
        context.SaveChanges();
        if (Obj.Emp_Id > 0)
        {
            UserLogin ObjUser = new UserLogin();

            ObjUser.Login_ID = txtEmailId.Text.Trim();
            ObjUser.Password = Constant.DefaultPassword;
            ObjUser.Login_ID = txtEmailId.Text.Trim();
            ObjUser.Emp_Id = Obj.Emp_Id;
            ObjUser.Full_Name = txtemloyeeName.Text.Trim();
            ObjUser.Role_Code = int.Parse(ddlRole.SelectedValue.ToString());
            ObjUser.IsEnabled = chk_Enable_Disable.Checked;
            ObjUser.Is_Active = true;
            ObjUser.Created_Date = dt;
            ObjUser.User_IP = UserIP;
            context.UserLogins.Add(ObjUser);
            context.SaveChanges();
            if (ObjUser.User_Code > 0)
            {
                ClosePopup();
                ResetControls();
                Success("Added Successfully");
            }

        }
    }
    private void Update()
    {
        int RoleId = Convert.ToInt32(ddlRole.SelectedValue == "" ? "0" : ddlRole.SelectedValue);
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        Setup_Employee Obj = context.Setup_Employee.FirstOrDefault(a => a.IsActive == true && a.Emp_Id == Id);
        Obj.EmployeeName = txtemloyeeName.Text.Trim();
        Obj.EmailId = txtEmailId.Text.Trim();
        Obj.PhoneNo = txtPhoneNo.Text.Trim();
        Obj.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
        Obj.DesignationId = NullInt;
        Obj.Address = txtAddress.Text.Trim();
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        context.SaveChanges();

        UserLogin ObjUser = context.UserLogins.FirstOrDefault(a => a.Is_Active == true && a.Emp_Id == Id);
        ObjUser.Login_ID = txtEmailId.Text.Trim();
        ObjUser.Role_Code = int.Parse(ddlRole.SelectedValue.ToString());
        ObjUser.Full_Name = txtemloyeeName.Text.Trim();
        ObjUser.IsEnabled = chk_Enable_Disable.Checked;
        ObjUser.Modified_Date = dt;
        ObjUser.Modified_By = UserId;
        context.SaveChanges();

        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }
    private string CheckNameIfExist(int Id, string EmailId)
    {
        string Msg = "";
        int Count = 0;
        try
        {

            Count = context.Setup_Employee.Where(x => x.IsActive == true && x.Emp_Id != Id && x.EmailId == EmailId).Count();
            if (Count > 0)
            {
                Msg = " Email / Login Id  already exist";
            }


            return Msg;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtLogininSearch.Text = txtUsernameSearch.Text = "";
        ResetControls();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }
    private void ResetControls()
    {
        hfId.Value = "";
        Is_ClickOnEdit = false;
        chk_Enable_Disable.Checked = true;
        divError_.Visible = false;
        lblError_.InnerText = "";
        divError.Visible = false;
        lblError.InnerText = "";
        txtEmailId.Text = txtAddress.Text = txtemloyeeName.Text = txtPhoneNo.Text = "";
        BindDropDown();
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {

            divError_.Visible = false;
            lblError_.InnerText = "";
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.Setup_Employee.FirstOrDefault(j => j.Emp_Id == hfRId);
            int RoleId = 0;
            if (List != null)
            {
                Is_ClickOnEdit = true;
                int UserId = 0;
                bool IsEnabled = false;
                var ListUser = context.UserLogins.FirstOrDefault(a => a.Is_Active == true && a.Emp_Id == hfRId);
                if (ListUser != null)
                {
                    UserId = ListUser.User_Code;
                    IsEnabled = Convert.ToBoolean(ListUser.IsEnabled);
                }



                txtEmailId.Text = List.EmailId == null ? "" : Convert.ToString(List.EmailId).Trim();
                txtPhoneNo.Text = List.PhoneNo == null ? "" : Convert.ToString(List.PhoneNo).Trim();
                txtAddress.Text = List.Address == null ? "" : Convert.ToString(List.Address).Trim();
                txtemloyeeName.Text = List.EmployeeName.ToString().Trim();
                ddlRole.SelectedValue = ListUser.Role_Code.ToString();
                ddlDepartment.SelectedValue = Convert.ToString(List.DepartmentId == null ? 0 : List.DepartmentId);
                //ddlDesignation.SelectedValue = Convert.ToString(List.DesignationId == null ? 0 : List.DesignationId);
                chk_Enable_Disable.Checked = IsEnabled;
                // ScriptManager.RegisterStartupScript(this, GetType(), "RoleChange(" + RoleId + ")", "RoleChange(" + RoleId + ");", true);

                hfId.Value = hfRId.ToString();
                Is_ClickOnEdit = false;

                OpenPopup();
            }
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int hfUserCode = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfUserCode")).Value);
            var List = context.UserLogins.FirstOrDefault(j => j.User_Code == hfUserCode);
            if (List != null)
            {
                string msg = IsTransactionExist(hfUserCode);
                if (msg == "")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        DateTime dt = DateTime.Now;
                        List.Is_Active = false;
                        List.Modified_By = UserId;
                        List.Modified_Date = dt;
                        context.SaveChanges();
                        int empId_ = List.Setup_Employee2.Emp_Id;
                        var list_ = context.Setup_Employee.Where(a => a.IsActive == true && a.Emp_Id == empId_).ToList();
                        list_.ForEach(a => { a.IsActive = false; a.ModifiedBy = UserId; a.ModifiedDate = dt; });
                        context.SaveChanges();
                        Success("Deleted successfully");
                        ResetControls();
                        scope.Complete();
                    }
                }
                else
                {
                    Error(msg);
                    ResetControls();
                }
            }
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    public string IsTransactionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_TicketMaster.Where(a => a.CreatedBy == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Transaction exist against this user";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }

    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }

    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    public void RemainOpen()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ModalRemainOpen()", "ModalRemainOpen();", true);
    }

}