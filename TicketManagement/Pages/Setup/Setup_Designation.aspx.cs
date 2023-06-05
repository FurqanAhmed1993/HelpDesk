using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;


public partial class Pages_Setup_Setup_Designation : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
     
    }

    private void BindRepeater()
    {
        try
        {

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
    
            string Designation = txtDesignationSearch.Text.ToString();
            var List = context.Setup_Designation.Where(a => a.IsActive == true
                 && (a.DesignationName.Contains(Designation) || Designation == String.Empty)
                )
                 .Select(c => new
                 {
                     DesignationId = c.DesignationId,
                     DesignationName = c.DesignationName,
                 }).OrderBy(a => a.DesignationName)
                .ToList();
            var List1 = List.OrderBy(a => a.DesignationName).Skip(skip).Take(pageSize).ToList();
            rpt.DataSource = List1;
            rpt.DataBind();
            PagingAndSorting.setPagingOptions(List.Count());


        }
        catch (Exception ex)
        {
        }
    }

    private void Add()
    {
        Setup_Designation Obj = new Setup_Designation();
        Obj.DesignationName = txtDesignation.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = DateTime.Now;
        Obj.UserIP = UserIP;
        context.Setup_Designation.Add(Obj);
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Added Successfully");


    }

    private string CheckNameIfExist(String Name, int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.Setup_Designation.Where(x => x.IsActive == true &&  x.DesignationId != Id && x.DesignationName == Name).Count();
            if (Count > 0)
            {
                Msg = "Designation already exist";
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
        txtDesignationSearch.Text = "";
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
            string Msg = CheckNameIfExist(txtDesignation.Text.Trim(), Id);
            if (Msg == "")
            {
                if (Id == 0)
                {
                    Add();
                }
                else
                {
                    Update();
                }
            }
            else
            {
                Error(Msg);
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void Update()
    {
        int Id = Convert.ToInt32(hfId.Value);
        Setup_Designation Obj = context.Setup_Designation.FirstOrDefault(j => j.DesignationId == Id);
        Obj.DesignationName = txtDesignation.Text.Trim();
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = DateTime.Now;
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
        BindDropDown();
        hfId.Value = "";
        txtDesignation.Text = "";
        BindRepeater();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.Setup_Designation.FirstOrDefault(j => j.DesignationId == hfRId);
            if (List != null)
            {
                hfId.Value = hfRId.ToString();
                txtDesignation.Text = List.DesignationName.Trim();
                OpenPopup();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.Setup_Designation.FirstOrDefault(j => j.DesignationId == hfRId);
            if (List != null)
            {
                string msg = IsTransectionExist(hfRId);
                if (msg == "")
                {
                    DateTime dt = DateTime.Now;
                    List.IsActive = false;
                    List.ModifiedBy = UserId;
                    List.ModifiedDate = dt;
                    List.UserIP = UserIP;
                    context.SaveChanges();
                    Success("Deleted successfully");
                    ResetControls();
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
        }
    }

    public string IsTransectionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.Setup_Employee.Where(a => a.DesignationId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "User exist against this designation";
            }
           
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }



}