using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;


public partial class Pages_Security_Setup_MenuItem : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindRepeater();
        }
    }

    private void BindDropDown()
    {
        var list = context.Setup_Application.Where(a => a.IsActive == true && a.ApplicationID == (int)Constant.Application.Ticket).OrderBy(a => a.ApplicationID).ToList();
        Common.BindDropDown(ddlCompany, list, "ApplicationName", "ApplicationID", false, false);
        Common.BindDropDown(ddlcompanySearch, list, "ApplicationName", "ApplicationID", false, false);
        ddlCompany_SelectedIndexChanged(null, null);
        ddlcompanySearch_SelectedIndexChanged(null, null);
    }

    private void BindRepeater()
    {
        try
        {

            divError_.Visible = false;
            lblError_.InnerText = "";
            int CId = Convert.ToInt32(ddlcompanySearch.SelectedValue == "" ? "0" : ddlcompanySearch.SelectedValue);
            int PMId = Convert.ToInt32(ddlParentMenuSearch.SelectedValue == "" ? "0" : ddlParentMenuSearch.SelectedValue);
            string Department = txtMenuItemSearch.Text.ToString();
            var List = context.MenuItems.Where(a => a.Is_Active == true
                 && (a.Menu_Item_Name.Contains(Department) || Department == String.Empty)
                 && (a.ApplicationID == CId)
                 && (a.Parent_Menu_Item_Code == PMId || PMId == 0))
                 .Select(c => new
                 {
                     ParentId = c.Parent_Menu_Item_Code == null ? c.Menu_Item_Code : c.Parent_Menu_Item_Code,
                     ParentMenu = c.Parent_Menu_Item_Code == null ? "" : context.MenuItems.FirstOrDefault(b => b.Menu_Item_Code == c.Parent_Menu_Item_Code).Menu_Item_Name,
                     MenuItemId = c.Menu_Item_Code,
                     CompanyName = context.Setup_Application.FirstOrDefault(a => a.ApplicationID == c.ApplicationID).ApplicationName,
                     MenuItem = c.Menu_Item_Name,
                     MenuURL = c.Menu_URL,
                     SortOrder = c.SortOrder,
                 }).OrderBy(a => a.CompanyName).ThenBy(a => a.ParentMenu).ThenBy(a => a.MenuItem)
                .ToList();
            rpt.DataSource = List;
            rpt.DataBind();

        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    int? NullInt = null;

    private void Add()
    {
        DateTime dt = DateTime.Now;
        DAL.MenuItem Obj = new DAL.MenuItem();
        Obj.Menu_Item_Name = txtMenu.Text.Trim();
        Obj.Menu_URL = txtURL.Text.Trim();
        Obj.Parent_Menu_Item_Code = ddlParentMenu.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlParentMenu.SelectedValue);
        Obj.Is_Displayed_In_Menu = IsDisplayInMenu.Checked;
        Obj.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
        Obj.ApplicationID = Convert.ToInt32(ddlCompany.SelectedValue);
        Obj.Created_Date = dt;
        Obj.Is_Active = true;
        Obj.User_IP = UserIP;
        context.MenuItems.Add(Obj);
        context.SaveChanges();
        ClosePopup();
        ResetControls();
        Success("Added Successfully");

    }

    private string CheckNameIfExist(String Name, int Id, int CompanyId)
    {
        string Msg = "";

        int Count = context.MenuItems.Where(x => x.Is_Active == true && x.ApplicationID == CompanyId && x.Menu_Item_Code != Id && x.Menu_Item_Name == Name).Count();
        if (Count > 0)
        {
            Msg = "Menu already exist";
        }

        return Msg;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtMenuItemSearch.Text = "";
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
            string Msg = "";// CheckNameIfExist(txtMenu.Text.Trim(), Id, Convert.ToInt32(ddlCompany.SelectedValue));
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
        var Obj = context.MenuItems.FirstOrDefault(j => j.Menu_Item_Code == Id);
        Obj.Menu_Item_Name = txtMenu.Text.Trim();
        Obj.Menu_URL = txtURL.Text.Trim();
        Obj.Parent_Menu_Item_Code = ddlParentMenu.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlParentMenu.SelectedValue);
        Obj.Is_Displayed_In_Menu = IsDisplayInMenu.Checked;
        Obj.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
        Obj.ApplicationID = Convert.ToInt32(ddlCompany.SelectedValue);
        Obj.Is_Active = true;
        Obj.Modified_Date = dt;
        Obj.User_IP = UserIP;
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
        txtMenu.Text = txtSortOrder.Text = txtURL.Text = "";
        //ddlCompany.SelectedValue = "0";
        ddlCompany_SelectedIndexChanged(null, null);
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
            var List = context.MenuItems.FirstOrDefault(j => j.Menu_Item_Code == hfRId);
            if (List != null)
            {
                txtMenu.Text = Convert.ToString(List.Menu_Item_Name == null ? "" : List.Menu_Item_Name).Trim();
                txtURL.Text = Convert.ToString(List.Menu_URL == null ? "" : List.Menu_URL).Trim();
                txtSortOrder.Text = List.SortOrder == null ? "" : Convert.ToString(List.SortOrder);
                ddlCompany.SelectedValue = Convert.ToString(List.ApplicationID);
                ddlCompany_SelectedIndexChanged(null, null);
                ddlParentMenu.SelectedValue = Convert.ToString(List.Parent_Menu_Item_Code == null ? 0 : List.Parent_Menu_Item_Code);
                IsDisplayInMenu.Checked = Convert.ToBoolean(List.Is_Displayed_In_Menu == null ? false : List.Is_Displayed_In_Menu);
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
            var List = context.MenuItems.FirstOrDefault(j => j.Menu_Item_Code == hfRId);
            if (List != null)
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    DateTime dt = DateTime.Now;
                    List.Is_Active = false;
                    List.Modified_Date = dt;
                    List.User_IP = UserIP;
                    context.SaveChanges();
                    Success("Deleted successfully");
                    ResetControls();
                    scope.Complete();
                }

            }
        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }

    protected void ddlcompanySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Id = Convert.ToInt32(ddlcompanySearch.SelectedValue);
        var li = context.MenuItems.Where(a => a.Is_Active == true && a.Parent_Menu_Item_Code == null && a.Menu_URL == "#" && a.ApplicationID == Id).ToList();
        Common.BindDropDown(ddlParentMenuSearch, li, "Menu_Item_Name", "Menu_Item_Code", true, false);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Id = Convert.ToInt32(ddlCompany.SelectedValue);
        var li = context.MenuItems.Where(a => a.Is_Active == true && a.Parent_Menu_Item_Code == null && a.Menu_URL == "#" && a.ApplicationID == Id).ToList();
        Common.BindDropDown(ddlParentMenu, li, "Menu_Item_Name", "Menu_Item_Code", true, false);
    }

}