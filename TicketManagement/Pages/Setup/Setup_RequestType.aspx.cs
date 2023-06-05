using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;


public partial class Pages_Setup_Setup_RequestType : Base
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
           
            string RequestType = txtRequestTypeSearch.Text.ToString();
            var List = context.TS_Setup_RequestType.Where(a => a.IsActive == true
                 && (a.RequestTypeName.Contains(RequestType) || RequestType == String.Empty)
                )
                 .Select(c => new
                 {
                     RequestTypeId = c.RequestTypeId,
                     RequestTypeName = c.RequestTypeName,
                 }).OrderBy(a => a.RequestTypeName)
                .ToList();
            var List1 = List.OrderBy(a => a.RequestTypeName).Skip(skip).Take(pageSize).ToList();
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
        TS_Setup_RequestType Obj = new TS_Setup_RequestType();
        Obj.RequestTypeName = txtRequestType.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.TS_Setup_RequestType.Add(Obj);
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
            int Count = context.TS_Setup_RequestType.Where(x => x.IsActive == true && x.RequestTypeId != Id && x.RequestTypeName == Name).Count();
            if (Count > 0)
            {
                Msg = "Request type already exist";
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
        txtRequestTypeSearch.Text = "";
        ResetControls();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblError.InnerText = "";
        try
        {
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckNameIfExist(txtRequestType.Text.Trim(), Id);
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
        TS_Setup_RequestType Obj = context.TS_Setup_RequestType.FirstOrDefault(j => j.RequestTypeId == Id);
        Obj.RequestTypeName = txtRequestType.Text.Trim();
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
        divError_.Visible = false;
        lblError_.InnerText = "";
        divError.Visible = false;
        lblError.InnerText = "";
        hfId.Value = "";
        txtRequestType.Text = "";
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
            var List = context.TS_Setup_RequestType.FirstOrDefault(j => j.RequestTypeId == hfRId);
            if (List != null)
            {
          
                txtRequestType.Text = Convert.ToString(List.RequestTypeName == null ? "" : List.RequestTypeName).Trim();
                hfId.Value = hfRId.ToString();
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
            var List = context.TS_Setup_RequestType.FirstOrDefault(j => j.RequestTypeId == hfRId);
            if (List != null)
            {
                string msg = IsTransectionExist(hfRId);
                if (msg == "")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
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
            int Count = context.TS_Setup_RequestTypeCategory.Where(x => x.RequestTypeId == Id && x.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "Category is exist against this Request Type.";
            }
            else
            {
                Count = context.TS_TicketMaster.Where(x => x.RequestTypeId == Id && x.IsActive == true).Count();
                if (Count > 0)
                {
                    Msg = "Ticket is exist against this Request Type.";
                }
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }


}