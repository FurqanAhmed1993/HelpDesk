using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Controls_Menu : System.Web.UI.UserControl
{

    public System.Text.StringBuilder menu = new System.Text.StringBuilder();

    List<RoleAccess_SelectByApplicationid_Result> menuList;

    public string IsAdmin, CanAddUpdate, CanView;

    protected void Page_Init(object sender, EventArgs e)
    {
        // check if the current user is logged in , and has permissions.
        AuthenticateUser();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TicketSystemEntities context = new TicketSystemEntities();
            Base Objbase = new Base();
            string UserId_ = Convert.ToString(Objbase.UserId);
            string EmployeeId_ = Convert.ToString(Objbase.EmployeeId);
            string RoleCode_ = Convert.ToString(Objbase.RoleCode);
            int? role = int.Parse(new Base().RoleCode);
            int UserCode = Objbase.UserId;
            int? ApplicationId = (int)Constant.Application.Ticket;
            menuList = context.RoleAccess_SelectByApplicationid(role, ApplicationId).Where(r => r.Is_Active == true && r.Is_Displayed_In_Menu == true && r.ApplicationID == ApplicationId).ToList();
            MakeMenuHTML();
        }
    }

    private void AuthenticateUser()
    {
        if (new Base().UserId == 0 || string.IsNullOrEmpty(new Base().RoleCode) == true)
            Response.Redirect("~/Login.aspx", true);
        else if (CheckPageAccess() == false)
        {
            Response.Write("UnAuthorized Access");
            Response.End();
            Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/ErrorPage.aspx", true);
            /*New Code*/
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    private bool CheckPageAccess()
    {
        TicketSystemEntities context = new TicketSystemEntities();
        Base Objbase = new Base();
        int? role = int.Parse(new Base().RoleCode);
        string url = Request.Url.AbsolutePath;
        RoleAccess roleAccess = context.RoleAccesses.FirstOrDefault(r => r.Is_Active == true && r.Role_Code == role && url.Contains(r.MenuItem.Menu_URL) == true);
        if (roleAccess == null || roleAccess.Has_Access == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void MakeMenuHTML()
    {
        List<RoleAccess_SelectByApplicationid_Result> menuTop = menuList.Where(m => m.Parent_Menu_Item_Code.ToString() == string.Empty || m.Parent_Menu_Item_Code == 0).ToList();
        for (int i = 0; i < menuTop.Count; i++)
        {
            menu.Append(@" <li class='dropdown'> ");
            if ((menuTop[i].Menu_URL == "#" || menuTop[i].Menu_URL == string.Empty) || menuTop[i].Has_Access == true)
            {
                string url = menuTop[i].Menu_URL == string.Empty ? "#" : menuTop[i].Menu_URL;
                List<RoleAccess_SelectByApplicationid_Result> menuChild = menuList.Where(m => m.Parent_Menu_Item_Code == menuTop[i].Menu_Item_Code).ToList();
                if (menuChild.Count > 0)
                {
                    menu.Append(@" <a data-toggle='dropdown' class='dropdown-toggle' href='" + url + "'>" + menuTop[i].Menu_Item_Name + " <b class='icon-angle-down'></b> </a>");
                    MakeSubMenuHTML(menuChild);
                }
                else
                {

                    menu.Append(@" <a  href='" + url + "'>" + menuTop[i].Menu_Item_Name + "  </a>");
                }
                menu.Append(@"</li>");
            }
        }
    }

    private void MakeSubMenuHTML(List<RoleAccess_SelectByApplicationid_Result> menuChild)
    {
        menu.Append(@"<div class='dropdown-menu'><ul>");
        for (int j = 0; j < menuChild.Count; j++)
        {
            if ((menuChild[j].Menu_URL == "#" || menuChild[j].Menu_URL == string.Empty) || menuChild[j].Has_Access == true)
            {
                string url = menuChild[j].Menu_URL == string.Empty ? "#" : menuChild[j].Menu_URL;
                menu.Append(@"<li><a href='" + url + "'>" + menuChild[j].Menu_Item_Name + "</a></li>");
                List<RoleAccess_SelectByApplicationid_Result> moreMenuChild = menuList.Where(m => m.Parent_Menu_Item_Code == menuChild[j].Menu_Item_Code).ToList();
                if (moreMenuChild.Count > 0)
                    MakeSubMenuHTML(moreMenuChild);
            }
        }
        menu.Append(@"</ul></div>  ");
    }

}