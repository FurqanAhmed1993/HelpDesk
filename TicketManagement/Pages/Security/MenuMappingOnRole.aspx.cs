using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Transactions;
using System.Data;

public partial class Pages_Security_MenuMappingOnRole : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindDropDown();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void BindDropDown()
    {
        var list = context.Setup_Application.Where(a => a.IsActive == true && a.ApplicationID == (int)Constant.Application.Ticket).OrderBy(a => a.ApplicationID).ToList();
        Common.BindDropDown(ddlcompany, list, "ApplicationName", "ApplicationID", false, false);
        ddlcompany_SelectedIndexChanged(null, null);
    }



    private void Update()
    {
        bool IsInsert = false;
        try
        {
            DateTime _DtNow = DateTime.Now;
            int _RolId = Convert.ToInt32(ddlRole.SelectedValue);
            int ApplicationID = Convert.ToInt32(ddlcompany.SelectedValue);

            using (TransactionScope scope = new TransactionScope())
            {
                var _RollSec_Setup_RoleMenuItemMapping = context.RoleAccesses.Where(m => m.Is_Active == true && m.Role_Code == _RolId && m.MenuItem.ApplicationID == ApplicationID).ToList();
                _RollSec_Setup_RoleMenuItemMapping.ForEach(a => { a.Is_Active = false; a.Modified_Date = _DtNow; a.User_IP = UserIP; a.Has_Access = false; });
                context.SaveChanges();
                foreach (TreeNode tn in TreeView1.CheckedNodes)
                {
                    int _MenuItemId = Convert.ToInt32(tn.Value);
                    bool _HasChild = context.MenuItems.Any(m => m.Is_Active == true && m.Parent_Menu_Item_Code == _MenuItemId);

                    if (tn.ChildNodes.Count >= 1)
                    {
                        RoleAccess _RoleMenuItemMapping = new RoleAccess();
                        _RoleMenuItemMapping.Menu_Item_Code = _MenuItemId;
                        _RoleMenuItemMapping.Role_Code = _RolId;
                        _RoleMenuItemMapping.Created_Date = _DtNow;
                        _RoleMenuItemMapping.Is_Active = true;
                        _RoleMenuItemMapping.User_IP = UserIP;
                        _RoleMenuItemMapping.Has_Access = true;
                        context.RoleAccesses.Add(_RoleMenuItemMapping);
                        context.SaveChanges();
                    }
                    else
                    {
                        int a = Convert.ToInt32(tn.Depth);
                        int _MenuItemFeatureId = Convert.ToInt32(tn.Value);
                        if (a == 0)
                        {
                            RoleAccess _RoleMenuItemMapping = new RoleAccess();
                            _RoleMenuItemMapping.Menu_Item_Code = _MenuItemId;
                            _RoleMenuItemMapping.Role_Code = _RolId;
                            _RoleMenuItemMapping.Created_Date = _DtNow;
                            _RoleMenuItemMapping.Is_Active = true;
                            _RoleMenuItemMapping.User_IP = UserIP;
                            _RoleMenuItemMapping.Has_Access = true;
                            context.RoleAccesses.Add(_RoleMenuItemMapping);
                            context.SaveChanges();
                        }

                        if (a == 1)
                        {
                            RoleAccess _RoleMenuItemMapping = new RoleAccess();
                            _RoleMenuItemMapping.Menu_Item_Code = _MenuItemId;
                            _RoleMenuItemMapping.Role_Code = _RolId;
                            _RoleMenuItemMapping.Created_Date = _DtNow;
                            _RoleMenuItemMapping.Is_Active = true;
                            _RoleMenuItemMapping.User_IP = UserIP;
                            _RoleMenuItemMapping.Has_Access = true;
                            context.RoleAccesses.Add(_RoleMenuItemMapping);
                            context.SaveChanges();
                        }
                    }
                }
                scope.Complete();
                IsInsert = true;

            }

            if (IsInsert == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenDialog('success','Saved Successfully','success')", "OpenDialog('success','Saved Successfully','success');", true);
                ddlRole.SelectedValue = "0";
                ddlRole_SelectedIndexChanged(null, null);
            }


        }
        catch (Exception ex) { }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Update();
        }
        catch (Exception ex) { }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TreeView1.Nodes.Clear();
            PopulateRootLevel();
        }
        catch (Exception ex) { }
    }


    #region POPULATE TREEVIEW

    private void PopulateRootLevel()
    {
        int CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
        int _RolId = Convert.ToInt32(ddlRole.SelectedValue);
        DataTable dt = Common.ToDataTable(context.MenuItems.Where(m => m.Is_Active == true && m.ApplicationID == CompanyId && m.Parent_Menu_Item_Code == null)
             .Select(m => new
             {
                 Id = m.Menu_Item_Code,
                 Value = m.Menu_Item_Name,
                 ParentMenuId = m.Parent_Menu_Item_Code == null ? 0 : m.Parent_Menu_Item_Code,
                 HasAccess = m.RoleAccesses.Any(im => im.Is_Active == true && m.Menu_Item_Code == im.Menu_Item_Code && im.Role_Code == _RolId),
                 SortNo = m.SortOrder,
             }).OrderBy(a => a.SortNo).ToList());

        PopulateNodes(dt, TreeView1.Nodes);
    }
    private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
    {

        int _RolId = Convert.ToInt32(ddlRole.SelectedValue);
        foreach (DataRow dr in dt.Rows)
        {
            int _Id = Convert.ToInt32(dr["Id"]);
            int ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]);
            TreeNode ParentMenuItem_Tn = new TreeNode();
            if (ParentMenuId == 0)
            {
                ParentMenuItem_Tn.Text = dr["Value"].ToString();
                ParentMenuItem_Tn.Value = _Id.ToString();
                ParentMenuItem_Tn.Checked = Convert.ToBoolean(dr["HasAccess"]);
                nodes.Add(ParentMenuItem_Tn);
            }
            bool _HasChild = context.MenuItems.Any(m => m.Is_Active == true && m.Parent_Menu_Item_Code == _Id);
            ParentMenuItem_Tn.PopulateOnDemand = _HasChild;
            ParentMenuItem_Tn.Expand();
            if (_HasChild)
            {
                DataTable dtchieldMenuItem = Common.ToDataTable(context.MenuItems.Where(m => m.Is_Active == true && m.Parent_Menu_Item_Code == _Id)
                 .Select(m => new
                 {
                     Id = m.Menu_Item_Code,
                     Value = m.Menu_Item_Name,
                     HasAccess = m.RoleAccesses.Any(im => im.Is_Active == true && m.Menu_Item_Code == im.Menu_Item_Code && im.Role_Code == _RolId),
                     SortNo = m.SortOrder,
                 }).OrderBy(a => a.SortNo).ToList());

                foreach (DataRow drchieldMenuItem in dtchieldMenuItem.Rows)
                {
                    int _IdchieldMenuItem = Convert.ToInt32(drchieldMenuItem["Id"]);
                    TreeNode chieldMenuItem_Tn = new TreeNode();
                    chieldMenuItem_Tn.Text = drchieldMenuItem["Value"].ToString();
                    chieldMenuItem_Tn.Value = _IdchieldMenuItem.ToString();
                    chieldMenuItem_Tn.Checked = Convert.ToBoolean(drchieldMenuItem["HasAccess"]);
                    ParentMenuItem_Tn.ChildNodes.Add(chieldMenuItem_Tn);
                }
            }
        }
    }


    #endregion
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlcompany.SelectedValue);
        int? ParentId = context.Setup_Application.FirstOrDefault(a => a.IsActive == true && a.ApplicationID == id).ParentId;
        if (ParentId != null)
        {
            var lista = context.Roles.Where(p => p.Is_Active == true && p.ApplicationId == ParentId).Select(p => new
                      {
                          Id = p.Role_Code,
                          Value = p.Role_Name,
                      })
                      .ToList().OrderBy(p => p.Value);
            Common.BindDropDown(ddlRole, lista, "Value", "Id", true, false);
            ddlRole_SelectedIndexChanged(null, null);
        }
        else
        {
            var lista = context.Roles.Where(p => p.Is_Active == true && p.ApplicationId == id).Select(p => new
                      {
                          Id = p.Role_Code,
                          Value = p.Role_Name,
                      })
                      .ToList().OrderBy(p => p.Value);
            Common.BindDropDown(ddlRole, lista, "Value", "Id", true, false);
            ddlRole_SelectedIndexChanged(null, null);
        }

    }
}