using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_Setup_Setup_Customer : Base
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


            string Customer = txtCustomerNameSearch.Text.ToString();
            string Contact = txtContactSearch.Text.Trim();
            string Email = txtEmailSearch.Text.Trim();
            var List = context.Customers.Where(a => a.IsActive == true
                 && (a.CustomerName.Contains(Customer) || Customer == String.Empty)
                 && (a.ContactNo == Contact || Contact == String.Empty)
                 && (a.EmailAddress == Email || Email == String.Empty))
                 .Select(c => new
                 {
                     CustomerId = c.CustomerId,
                     CustomerName = c.CustomerName,
                     ContactNo = c.ContactNo,
                     EmailAddress = c.EmailAddress,
                     Address = c.Address,
                     CreatedDate = c.CreatedDate,
                 }).OrderBy(a => a.CreatedDate)
                .ToList();
            var List1 = List.OrderByDescending(a => a.CreatedDate).Skip(skip).Take(pageSize).ToList();
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
        Customer Obj = new Customer();
        Obj.CustomerName = txtCustomerName.Text.Trim();
        Obj.ContactNo = txtContactNo.Text.Trim();
        Obj.EmailAddress = txtEmail.Text.Trim();
        Obj.Address = txtAddress.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIp = UserIP;
        context.Customers.Add(Obj);
        context.SaveChanges();

        ClosePopup();
        ResetControls();
        Success("Added Successfully");

    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        Customer Obj = context.Customers.FirstOrDefault(j => j.CustomerId == Id);
        Obj.CustomerName = txtCustomerName.Text.Trim();
        Obj.ContactNo = txtContactNo.Text.Trim();
        Obj.EmailAddress = txtEmail.Text.Trim();
        Obj.Address = txtAddress.Text.Trim();
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedDate = dt;
        Obj.UserIp = UserIP;
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckEmailNumberIfExist(Id,txtContactNo.Text.Trim(),txtEmail.Text.Trim());
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

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Edit":
                    int hfRId = int.Parse(e.CommandArgument.ToString());
                    var List = context.Customers.FirstOrDefault(j => j.CustomerId == hfRId);
                    if (List != null)
                    {
                        hfId.Value = hfRId.ToString();
                        txtCustomerName.Text = List.CustomerName.Trim();
                        txtContactNo.Text = List.ContactNo.Trim();
                        txtEmail.Text = List.EmailAddress.Trim();
                        txtAddress.Text = List.Address.Trim();
                        OpenPopup();
                    }
                    break;
                case "Delete":
                    int hfCustId = int.Parse(e.CommandArgument.ToString());
                    string msg = IsTransactionExist(hfCustId);
                    if (msg == "")
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var ListCust = context.Customers.FirstOrDefault(j => j.CustomerId == hfCustId);
                            DateTime dt = DateTime.Now;
                            ListCust.IsActive = false;
                            ListCust.ModifiedBy = UserId;
                            ListCust.ModifiedDate = dt;
                            ListCust.UserIp = UserIP;

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
                    break;
               
            }
        }
        catch (Exception ex)
        {
          
        }
    }
    public string IsTransactionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_TicketMaster.Where(a => a.CustomerId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Ticket is exist against this Customer. Unable to delete this Customer.";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }

    private string CheckEmailNumberIfExist(int Id, string Contact, string Email)
    {
        string Msg = "";
        try
        {
            int Count = context.Customers.Where(x => x.IsActive == true && x.CustomerId != Id && x.ContactNo == Contact).Count();
            if (Count > 0)
            {
                Msg = "Customer already exists with this Contact Number.";
            }
            else
            {
                if (Email != "")
                {
                    Count = context.Customers.Where(x => x.IsActive == true && x.CustomerId != Id && x.EmailAddress == Email).Count();

                    if (Count > 0)
                    {
                        Msg = "Customer already exists with this Email.";

                    } 
                }

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
        hfId.Value = "";
        txtCustomerNameSearch.Text = txtCustomerName.Text = txtContactSearch.Text = 
        txtContactNo.Text = txtEmailSearch.Text = txtEmail.Text = txtAddress.Text = "";
        //isDefault.Checked = false;
        BindRepeater();
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