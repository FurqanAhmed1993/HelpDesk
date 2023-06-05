using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Transactions;
using System.Data;

public partial class Pages_Setup_Setup_RequestTypeSubcategory : Base
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
        try
        {
            var Category = context.TS_Setup_RequestTypeCategory.Where(a => a.IsActive == true).ToList();
            Common.BindDropDown(ddlCategory, Category, "CategoryName", "CategoryId", Category.Count == 1 ? false : true, false);
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
            string ServiceCategory = txtSubcategorySearch.Text.ToString();

            var List = context.TS_Setup_RequestTypeSubcategory.Where(a => a.IsActive == true
                  && (a.SubcategoryName.Contains(ServiceCategory) || ServiceCategory == String.Empty))
                 .Select(subcategory => new
                 {
                     SubcategoryId = subcategory.SubcategoryId,
                     SubcategoryName = subcategory.SubcategoryName,
                     ResolutionTime = subcategory.ResolutionTime,
                     CategoryId = subcategory.TS_Setup_RequestTypeCategory.CategoryId,
                     CategoryName = subcategory.TS_Setup_RequestTypeCategory.CategoryName,
                 }).ToList();
            var List1 = List.OrderBy(a => a.CategoryName).Skip(skip).Take(pageSize).ToList();
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
    //protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int requestId = int.Parse(ddlRequestType.SelectedValue);
    //        var Category = context.TS_Setup_RequestTypeCategory.Where(a => a.IsActive == true && a.RequestTypeId == requestId).ToList();
    //        Common.BindDropDown(ddlCategory, Category, "CategoryName", "CategoryId", Category.Count == 1 ? false : true, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        divError_.Visible = true;
    //        lblError_.InnerText = "Error : " + ex.Message;
    //    }
    //}
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
                    Label lblSubcategoryName = (Label)e.Item.FindControl("lblSubcategoryName");
                    Label lblResolutionTime = (Label)e.Item.FindControl("lblResolutionTime");
                    HiddenField CategoryId = (HiddenField)e.Item.FindControl("hfCategoryId");

                    int hfRId = Int32.Parse(e.CommandArgument.ToString());
                    hfId.Value = hfRId.ToString();
                    txtSubcategory.Text = lblSubcategoryName.Text;
                    //txtResolutionTime.Text = lblResolutionTime.Text;
                    ddlCategory.SelectedValue = CategoryId.Value;
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
                        var List = context.TS_Setup_RequestTypeSubcategory.FirstOrDefault(j => j.SubcategoryId == SubcategoryId);
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
            int categoryid = int.Parse(ddlCategory.SelectedValue);
            string Msg = CheckNameIfExist(txtSubcategory.Text.Trim(), Id, categoryid);
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
    private void Add()
    {
        DateTime dt = DateTime.Now;
        TS_Setup_RequestTypeSubcategory Obj = new TS_Setup_RequestTypeSubcategory();
        Obj.SubcategoryName = txtSubcategory.Text.Trim();
        Obj.CategoryId = int.Parse(ddlCategory.SelectedValue);
        Obj.ResolutionTime = 0;// int.Parse(txtResolutionTime.Text.Trim());
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIp = UserIP;
        context.TS_Setup_RequestTypeSubcategory.Add(Obj);
        context.SaveChanges();
        if (Obj.SubcategoryId > 0)
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
        TS_Setup_RequestTypeSubcategory Obj = context.TS_Setup_RequestTypeSubcategory.FirstOrDefault(j => j.SubcategoryId == Id);
        Obj.SubcategoryName = txtSubcategory.Text.Trim();
        Obj.ResolutionTime = 0;//int.Parse(txtResolutionTime.Text.Trim());
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        Obj.UserIp = UserIP;
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }
    public string IsTransactionExist(int Id)
    {
        string Msg = "";
        try
        {

            int Count = context.TS_TicketMaster.Where(a => a.RequestTypeSubCategoryId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Ticket exist against this Subcategory";
            }

            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }
    private string CheckNameIfExist(String Name, int Id, int categoryid)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_RequestTypeSubcategory.Where(x => x.IsActive == true && x.SubcategoryId != Id && x.SubcategoryName == Name && x.CategoryId == categoryid).Count();
            if (Count > 0)
            {
                Msg = "Subcategory already exist";
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
            txtSubcategory.Text = "";
            //txtResolutionTime.Text = "";
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