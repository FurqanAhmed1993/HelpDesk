using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;


public partial class Pages_Setup_Setup_TicketLevel : Base
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

            string Name = txtLevelSearch.Text.ToString();
            var List = context.TS_Setup_TicketLevel.Where(a => a.IsActive == true
                 && (a.LevelName.Contains(Name) || Name == String.Empty)
                 )
                 .Select(c => new
                 {
                     LevelId = c.LevelId,
                     Level = c.LevelName,
                 }).OrderBy(a => a.Level)
                .ToList();
            var List1 = List.OrderBy(a => a.Level).Skip(skip).Take(pageSize).ToList();
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
        TS_Setup_TicketLevel Obj = new TS_Setup_TicketLevel();
        Obj.LevelName = txtLevel.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = DateTime.Now;
        Obj.UserIP = UserIP;
        context.TS_Setup_TicketLevel.Add(Obj);
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
            int Count = context.TS_Setup_TicketLevel.Where(x => x.IsActive == true  && x.LevelId != Id && x.LevelName == Name).Count();
            if (Count > 0)
            {
                Msg = "Level already exist";
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
        txtLevelSearch.Text = "";
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
            divError.Visible = false;
            lblError.InnerText = "";
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Msg = CheckNameIfExist(txtLevel.Text.Trim(), Id);
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
            divError.Visible = true;
            lblError.InnerText = "Error : " + ex.Message;
        }

    }

    private void Update()
    {
        int Id = Convert.ToInt32(hfId.Value);
        TS_Setup_TicketLevel Obj = context.TS_Setup_TicketLevel.FirstOrDefault(j => j.LevelId == Id);
        Obj.LevelName = txtLevel.Text.Trim();
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
        txtLevel.Text = "";
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
            var List = context.TS_Setup_TicketLevel.FirstOrDefault(j => j.LevelId == hfRId);
            if (List != null)
            {
                txtLevel.Text = List.LevelName.Trim();
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
            var List = context.TS_Setup_TicketLevel.FirstOrDefault(j => j.LevelId == hfRId);
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
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    public string IsTransectionExist(int Id)
    {
        string Msg = "";
        try
        {
            //int Count = context.TS_Setup_LevelDesignationMapping.Where(a => a.LevelId == Id && a.IsActive == true).Count();
            //if (Count > 0)
            //{
            //    Msg = "Level - Designation Mapping exist against this Level";
            //}
            //else
            //{
            //    Count = context.TS_Setup_TAT.Where(a => a.LevelId == Id && a.IsActive == true).Count();
            //    if (Count > 0)
            //    {
            //        Msg = "TAT exist against this Level";
            //    }
            //    else
            //    {
            //        Count = context.TS_TicketMaster.Where(a => a.LevelId == Id && a.IsActive == true).Count();
            //        if (Count > 0)
            //        {
            //            Msg = "Ticket exist against this Level";
            //        }
            //    }
            //}
            return Msg;
        }
        catch (Exception ex)
        {
            return Msg;
        }
    }



}