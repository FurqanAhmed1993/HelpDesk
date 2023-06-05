using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;

public partial class Pages_Setup_Setup_TicketType : Base
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


          
            string Name = txtTicketTypeSearch.Text.ToString();
            var List = context.TS_Setup_TicketType.Where(a => a.IsActive == true
                 && (a.TicketTypeName.Contains(Name) || Name == String.Empty)
                )
                 .Select(c => new
                 {
                     TicketTypeId = c.TicketTypeId,
                     TicketTypeName = c.TicketTypeName,
                 }).OrderBy(a => a.TicketTypeName)
                .ToList();
            var List1 = List.OrderBy(a => a.TicketTypeName).Skip(skip).Take(pageSize).ToList();
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
        TS_Setup_TicketType Obj = new TS_Setup_TicketType();
        Obj.TicketTypeName = txtTicketType.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = DateTime.Now;
        Obj.UserIP = UserIP;
        context.TS_Setup_TicketType.Add(Obj);
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
            int Count = context.TS_Setup_TicketType.Where(x => x.IsActive == true  && x.TicketTypeId != Id && x.TicketTypeName == Name).Count();
            if (Count > 0)
            {
                Msg = "Ticket Type already exist";
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
        txtTicketTypeSearch.Text = "";
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
            string Msg = CheckNameIfExist(txtTicketType.Text.Trim(), Id);
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
        TS_Setup_TicketType Obj = context.TS_Setup_TicketType.FirstOrDefault(j => j.TicketTypeId == Id);
        Obj.TicketTypeName = txtTicketType.Text.Trim();
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
        txtTicketType.Text = "";
        BindRepeater();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.TS_Setup_TicketType.FirstOrDefault(j => j.TicketTypeId == hfRId);
            if (List != null)
            {
                txtTicketType.Text = List.TicketTypeName.Trim();
                hfId.Value = hfRId.ToString();
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
            var List = context.TS_Setup_TicketType.FirstOrDefault(j => j.TicketTypeId == hfRId);
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

            int Count = context.TS_TicketMaster.Where(a => a.TicketTypeId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Ticket is exist against this Type";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }
}