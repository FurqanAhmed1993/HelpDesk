using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;

public partial class Pages_Setup_Setup_Department : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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


            string Department = txtDepartmentSearch.Text.ToString();
            string Prefix = txtPrefixSearch.Text.Trim();
            var List = context.Setup_Department.Where(a => a.IsActive == true
                 && (a.DepartmentName.Contains(Department) || Department == String.Empty)
                 && (a.DepartmentPrefix.Contains(Prefix) || Prefix == String.Empty))
                 .Select(c => new
                 {
                     DepartmentId = c.DepartmentId,
                     IsDefault = c.IsDefault == true ? "1" : "0",
                     DepartmentName = c.DepartmentName,
                     DepartmentPrefix = c.DepartmentPrefix,
                     Email = c.Email,
                     Description = c.Description,
                 }).OrderBy(a => a.DepartmentName)
                .ToList();
            var List1 = List.OrderBy(a => a.DepartmentName).Skip(skip).Take(pageSize).ToList();
            rpt.DataSource = List1;
            rpt.DataBind();
            PagingAndSorting.setPagingOptions(List.Count());


        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_Department Obj = new Setup_Department();
        Obj.DepartmentName = txtDepartment.Text.Trim();
        Obj.DepartmentPrefix = txtPrefix.Text.Trim();
        Obj.Description = txtDescription.Text.Trim();
        Obj.Email = txtEmailId.Text;
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.Setup_Department.Add(Obj);
        context.SaveChanges();

        ClosePopup();
        ResetControls();
        Success("Added Successfully");



    }

    private string CheckNameIfExist(String Name, int Id, int CompanyId, string Prefix, string Email)
    {
        string Msg = "";
        try
        {
            int Count = context.Setup_Department.Where(x => x.IsActive == true && x.DepartmentId != Id && x.DepartmentName == Name).Count();
            if (Count > 0)
            {
                Msg = "Department already exist";
            }
            else
            {
                Count = context.Setup_Department.Where(x => x.IsActive == true && x.DepartmentId != Id && x.DepartmentPrefix == Prefix).Count();

                if (Count > 0)
                {
                    Msg = "Prefix already exist";

                }
               
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
        txtDepartmentSearch.Text = txtPrefixSearch.Text = "";
        ResetControls();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckNameIfExist(txtDepartment.Text.Trim(), Id, 0, txtPrefix.Text.Trim(), txtEmailId.Text.Trim());
            if (Msg == "")
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (Id == 0)
                    {
                        Add();
                    }
                    else
                    {
                        Update();
                    }
                    scope.Complete();
                }
            }
            else
            {
                Error(Msg);
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = "Error : " + ex.Message;
        }

    }

    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        Setup_Department Obj = context.Setup_Department.FirstOrDefault(j => j.DepartmentId == Id);
        Obj.DepartmentName = txtDepartment.Text.Trim();
        Obj.DepartmentPrefix = txtPrefix.Text.Trim();
        Obj.Description = txtDescription.Text.Trim();
        Obj.Email = txtEmailId.Text;
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        Obj.UserIP = UserIP;
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }

    private void ResetControls()
    {
        hfId.Value = "";
        txtDepartment.Text = txtDescription.Text = txtEmailId.Text = txtPrefix.Text = "";
        //isDefault.Checked = false;
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
            var List = context.Setup_Department.FirstOrDefault(j => j.DepartmentId == hfRId);
            if (List != null)
            {
                hfId.Value = hfRId.ToString();
                txtDepartment.Text = List.DepartmentName.Trim();
                txtDescription.Text = List.Description.Trim();
                txtEmailId.Text = List.Email.Trim();
                txtPrefix.Text = List.DepartmentPrefix.Trim();
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
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            string msg = IsTransectionExist(hfRId);
            if (msg == "")
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var List = context.Setup_Department.FirstOrDefault(j => j.DepartmentId == hfRId);
                    DateTime dt = DateTime.Now;
                    List.IsActive = false;
                    List.ModifiedBy = UserId;
                    List.ModifiedDate = dt;
                    List.UserIP = UserIP;
                   
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
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    public string IsTransectionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.Setup_Employee.Where(a => a.DepartmentId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "User is exist against this department. Unable to delete this department.";
            }
            else
            {
                Count = context.TS_TaskMaster.Where(a => a.DepartmentId == Id && a.IsActive == true).Count();
                if (Count > 0)
                {
                    Msg = "Task is exist against this department. Unable to delete this department.";
                }
                else
                {
                    Count = context.TS_TicketMaster.Where(a => a.DepartmentId == Id && a.IsActive == true).Count();
                    if (Count > 0)
                    {
                        Msg = "Ticket is exist against this department. Unable to delete this department.";
                    }

                }
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lbldesigntion = (Label)e.Item.FindControl("lbldesignation");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                int IsDefault = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfIsDefault")).Value);
                int _Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfRId")).Value);

                if (IsDefault == 1)
                {
                    lbDelete.Visible = false;
                }
                else
                {
                    lbDelete.Visible = true;
                }
                //List<TS_Setup_DepartmentDesignationMapping> mapping = context.TS_Setup_DepartmentDesignationMapping.Where(a => a.IsActive == true && a.DepartmentId == _Id).ToList();
                //string name = "<ul style='text-align: justify;margin-left: 11px; '>";
                //for (int i = 0; i < mapping.Count; i++)
                //{
                //    int? designationid = mapping[i].DesignationId;
                //    name = name + "<li>";
                //    Setup_Designation designation = context.Setup_Designation.FirstOrDefault(a => a.IsActive == true && a.DesignationId == designationid);
                //    name = name + (designation == null ? "" : designation.DesignationName) + "</li>";
                //}
                //name = name + "</ul>";
                //lbldesigntion.Text = name;
            }
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }


}