using DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Setup_Setup_City : Base
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txtCityNameSearch.Text = "";
        ResetControls();
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

            string Name = txtCityNameSearch.Text.ToString();
            var List = context.TS_Setup_City.Where(a => a.IsActive == true
                 && (a.CityName.Contains(Name) || Name == String.Empty)
                )
                 .Select(c => new
                 {
                     CityId = c.CityId,
                     CityName = c.CityName,
                 }).OrderBy(a => a.CityName)
                .ToList();
            var List1 = List.OrderBy(a => a.CityName).Skip(skip).Take(pageSize).ToList();
            rpt.DataSource = List1;
            rpt.DataBind();
            PagingAndSorting.setPagingOptions(List.Count());


        }
        catch (Exception ex)
        {
        }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.TS_Setup_City.FirstOrDefault(j => j.CityId == hfRId);
            if (List != null)
            {
                txtCityName.Text = List.CityName.Trim();
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
            var List = context.TS_Setup_City.FirstOrDefault(j => j.CityId == hfRId);
            if (List != null)
            {
                string msg = "";//IsTransectionExist(hfRId);
                if (msg == "")
                {
                    DateTime dt = DateTime.Now;
                    List.IsActive = false;
                    List.ModifiedBy = UserId;
                    List.ModifiedAt = dt;
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

    private string CheckNameIfExist(String Name, int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_City.Where(x => x.IsActive == true && x.CityId != Id && x.CityName == Name).Count();
            if (Count > 0)
            {
                Msg = "City Name already exist";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckNameIfExist(txtCityName.Text.Trim(), Id);
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

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }

    private void Add()
    {
        TS_Setup_City Obj = new TS_Setup_City();
        Obj.CityName = txtCityName.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedAt = DateTime.Now;
        Obj.UserIP = UserIP;
        context.TS_Setup_City.Add(Obj);
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Added Successfully");
    }

    private void Update()
    {
        int Id = Convert.ToInt32(hfId.Value);
        TS_Setup_City Obj = context.TS_Setup_City.FirstOrDefault(j => j.CityId == Id);
        Obj.CityName = txtCityName.Text.Trim();
        Obj.IsActive = true;
        Obj.ModifiedBy = UserId;
        Obj.ModifiedAt = DateTime.Now;
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

    private void ResetControls()
    {
        hfId.Value = "";
        txtCityName.Text = "";
        BindRepeater();
    }

    public string IsTransectionExist(int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_City.Where(a => a.CityId == Id && a.IsActive == true).Count();
            if (Count > 0)
            {
                Msg = "City is exist against this Name";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }
}