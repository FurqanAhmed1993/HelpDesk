using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Transactions;

public partial class Pages_Setup_Setup_TypeOfIssue : Base
{
    TicketSystemEntities context = new TicketSystemEntities();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
        }
        PagingHandler();
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
            string typeOfIssue = txtProductSearch.Text.ToString();

            var List = context.TS_Setup_TypeOfIssue.Where(a => a.IsActive == true
                   && (a.TypeOfIssue.Contains(typeOfIssue) || typeOfIssue == String.Empty))
                 .Select(issue => new
                 {
                     TypeOfIssueId = issue.TypeOfIssueId,
                     TypeOfIssue = issue.TypeOfIssue,
                 }).ToList();
            var List1 = List.OrderBy(a => a.TypeOfIssue).Skip(skip).Take(pageSize).ToList();
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
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txtProductSearch.Text = "";
        ResetControls();
    }

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Edit":
                    divError_.Visible = false;
                    lblError_.InnerText = "";
                    Label lblTypeOfIssue = (Label)e.Item.FindControl("lblTypeOfIssue");

                    int hfRId = Int32.Parse(e.CommandArgument.ToString());
                    hfId.Value = hfRId.ToString();
                    txtProduct.Text = lblTypeOfIssue.Text;
                    OpenPopup();
                    break;

                case "Delete":
                    DateTime dt = DateTime.Now;
                    divError_.Visible = false;
                    lblError_.InnerText = "";
                    int typeOfIssueId = Int32.Parse(e.CommandArgument.ToString());
                    string msg = IsTransactionExist(typeOfIssueId);
                    if (msg == "")
                    {
                        var List = context.TS_Setup_TypeOfIssue.FirstOrDefault(j => j.TypeOfIssueId == typeOfIssueId);
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
                    break;
            }
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblError.InnerText = "";
        try
        {
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckNameIfExist(txtProduct.Text.Trim(), Id);
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
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }

    private void Add()
    {
        DateTime dt = DateTime.Now;
        TS_Setup_TypeOfIssue Obj = new TS_Setup_TypeOfIssue();
        Obj.TypeOfIssue = txtProduct.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.TS_Setup_TypeOfIssue.Add(Obj);
        context.SaveChanges();
        if (Obj.TypeOfIssueId > 0)
        {
            ClosePopup();
            ResetControls();
            Success("Added Successfully");
        }
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        TS_Setup_TypeOfIssue Obj = context.TS_Setup_TypeOfIssue.FirstOrDefault(j => j.TypeOfIssueId == Id);
        Obj.TypeOfIssue = txtProduct.Text.Trim();
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        Obj.UserIP = UserIP;
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }
    private string CheckNameIfExist(String Name, int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_TypeOfIssue.Where(x => x.IsActive == true && x.TypeOfIssueId != Id && x.TypeOfIssue == Name).Count();
            if (Count > 0)
            {
                Msg = "Product already exist";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string IsTransactionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_ProductSubCategory.Where(a => a.TypeOfIssueId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Subcategory exist against this Product";
            }
            else
            {
                Count = context.TS_TicketMaster.Where(a => a.TypeOfIssueId == Id && a.IsActive == true).Count();
                if (Count > 0)
                {
                    Msg = "Ticket exist against this Product";
                }
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }
    private void ResetControls()
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
            divError.Visible = false;
            lblError.InnerText = "";
            hfId.Value = "";
            txtProduct.Text = "";
            BindRepeater();
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
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

    
}