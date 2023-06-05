using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Transactions;
using System.Data;

public partial class Pages_Setup_Setup_ProductSubCategory : Base
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
            BindDropDown();
            BindRepeater();
        }
        PagingHandler();
    }
    public void BindDropDown()
    {
        try
        {
            var Category = context.TS_Setup_TypeOfIssue.Where(a => a.IsActive == true).ToList();
            Common.BindDropDown(ddlTypeOfIssue, Category, "TypeOfIssue", "TypeOfIssueId", Category.Count == 1 ? false : true, false);
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
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
            string productSubCategory = txtSubcategorySearch.Text.ToString();

            var List = context.TS_Setup_ProductSubCategory.Where(a => a.IsActive == true
                  && (a.ProductSubCategory.Contains(productSubCategory) || productSubCategory == String.Empty))
                 .Select(subcategory => new
                 {
                     ProductSubCategoryId = subcategory.ProductSubCategoryId,
                     ProductSubCategory = subcategory.ProductSubCategory,
                     TypeOfIssueId = subcategory.TS_Setup_TypeOfIssue.TypeOfIssueId,
                     TypeOfIssue = subcategory.TS_Setup_TypeOfIssue.TypeOfIssue,
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
        txtSubcategorySearch.Text = "";
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
                    Label lblProductSubCategory = (Label)e.Item.FindControl("lblProductSubCategory");
                    HiddenField hfTypeOfIssueId = (HiddenField)e.Item.FindControl("hfTypeOfIssueId");

                    int hfRId = Int32.Parse(e.CommandArgument.ToString());
                    hfId.Value = hfRId.ToString();
                    txtProductSubCategory.Text = lblProductSubCategory.Text;
                    ddlTypeOfIssue.SelectedValue = hfTypeOfIssueId.Value;
                    OpenPopup();
                    break;

                case "Delete":
                    DateTime dt = DateTime.Now;
                    divError_.Visible = false;
                    lblError_.InnerText = "";
                    int SubcategoryId = Int32.Parse(e.CommandArgument.ToString());
                    string msg = IsTransactionExist(SubcategoryId);
                    if (msg == "")
                    {
                        var List = context.TS_Setup_ProductSubCategory.FirstOrDefault(j => j.ProductSubCategoryId == SubcategoryId);
                        List.IsActive = false;
                        List.ModifiedBy = UserId;
                        List.ModifiedDate = dt;
                        List.UserIp = UserIP;
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
            int typeOfIssueId = int.Parse(ddlTypeOfIssue.SelectedValue);
            string Msg = CheckNameIfExist(txtProductSubCategory.Text.Trim(), Id, typeOfIssueId);
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
        TS_Setup_ProductSubCategory Obj = new TS_Setup_ProductSubCategory();
        Obj.ProductSubCategory = txtProductSubCategory.Text.Trim();
        Obj.TypeOfIssueId = int.Parse(ddlTypeOfIssue.SelectedValue);
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIp = UserIP;
        context.TS_Setup_ProductSubCategory.Add(Obj);
        context.SaveChanges();
        if (Obj.ProductSubCategoryId > 0)
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
        TS_Setup_ProductSubCategory Obj = context.TS_Setup_ProductSubCategory.FirstOrDefault(j => j.ProductSubCategoryId == Id);
        Obj.ProductSubCategory = txtProductSubCategory.Text.Trim();
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        Obj.UserIp = UserIP;
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }
    public string IsTransactionExist(int Id)
    {
        string Msg = "";
        try
        {

            int Count = context.TS_TicketMaster.Where(a => a.ProductSubCategoryId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Ticket exists against this Subcategory";
            }

            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }
    private string CheckNameIfExist(String Name, int Id, int typeOfIssueId)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_ProductSubCategory.Where(x => x.IsActive == true && x.ProductSubCategoryId != Id && x.ProductSubCategory == Name && x.TypeOfIssueId == typeOfIssueId).Count();
            if (Count > 0)
            {
                Msg = "Product Subcategory is already exist";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return ex.ToString();
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
            txtProductSubCategory.Text = "";
            BindDropDown();
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