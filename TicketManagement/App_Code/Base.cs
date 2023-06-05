using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO.Compression;

/// <summary>
/// Summary description for Base
/// </summary>
public class Base : System.Web.UI.Page
{
    public string GetSecurityKey()
    {
        string key = GetCookie("PSW_sesk");
        if (key != null)
            return key;
        else
        {
            key = Guid.NewGuid().ToString();
            SaveCookie("PSW_sesk", key);
            return key;
        }
    }

    public int Regionid
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_Region")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_Region"));
        }
        set { SaveCookie("PSW_Region", value.ToString()); }
    }
  
    public int UserId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_UserId")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_UserId"));
        }
        set { SaveCookie("PSW_UserId", value.ToString()); }
    }

    public int CustomerId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_ClientId")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_ClientId"));
        }
        set { SaveCookie("PSW_ClientId", value.ToString()); }
    }

    public int EmployeeId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_EmployeeId")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_EmployeeId"));
        }
        set { SaveCookie("PSW_EmployeeId", value.ToString()); }
    }

    public int? CalID
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_CalID")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_CalID"));
        }
        set { SaveCookie("PSW_CalID", value.ToString()); }
    }
    
    public string FullName
    {
        get { return GetCookie("PSW_fullname"); }
        set { SaveCookie("PSW_fullname", value); }
    }

    public string EmailAddress
    {
        get { return GetCookie("PSW_emailaddress"); }
        set { SaveCookie("PSW_emailaddress", value); }
    }

    public bool IsSuperAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_issuperadmin")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_issuperadmin"));
        }
        set { SaveCookie("PSW_issuperadmin", value.ToString()); }
    }

    public bool IsAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_isadmin")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_isadmin"));
        }
        set { SaveCookie("PSW_isadmin", value.ToString()); }
    }

    public bool IsEmployee
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_IsEmployee")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_IsEmployee"));
        }
        set { SaveCookie("PSW_IsEmployee", value.ToString()); }
    }
    public bool IsLevel_1
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_IsLevel_1")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_IsLevel_1"));
        }
        set { SaveCookie("PSW_IsLevel_1", value.ToString()); }
    }

    public bool IsIncharge
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_IsIncharge")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_IsIncharge"));
        }
        set { SaveCookie("PSW_IsIncharge", value.ToString()); }
    }

    public bool IsClient
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_IsClient")))
                return false;
            else
                return bool.Parse(GetCookie("PSW_IsClient"));
        }
        set { SaveCookie("PSW_IsClient", value.ToString()); }
    }

    public string RoleCode
    {
        get { return GetCookie("PSW_rolecode"); }
        set { SaveCookie("PSW_rolecode", value); }
    }

    public int DepartmentId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_DepartmentId")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_DepartmentId"));
        }
        set { SaveCookie("PSW_DepartmentId", value.ToString()); }
    }

    public int DesignationId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("PSW_DesignationId")))
                return 0;
            else
                return int.Parse(GetCookie("PSW_DesignationId"));
        }
        set { SaveCookie("PSW_DesignationId", value.ToString()); }
    }

    public int LevelId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("TKT_LevelId")))
                return 0;
            else
                return int.Parse(GetCookie("TKT_LevelId"));
        }
        set { SaveCookie("TKT_LevelId", value.ToString()); }
    }

    public int LevelSortNo
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("TKT_LevelSortNo")))
                return 0;
            else
                return int.Parse(GetCookie("TKT_LevelSortNo"));
        }
        set { SaveCookie("TKT_LevelSortNo", value.ToString()); }
    }

    public string UserIP
    {
        get { return Context.Request.UserHostAddress; }
    }

    private bool ValidateError(string errorMessage)
    {
        // exclude common backend exceptions
        if (errorMessage == "File does not exist.")
            return false;
        else
            return true;
    }

    private Exception SetExceptionText()
    {
        Exception ex = HttpContext.Current.Server.GetLastError();
        if (ex != null)
        {
            if (ex.GetBaseException() != null)
            {
                ex = ex.GetBaseException();
                // lblError.Text = ex.Message;
            }
        }
        return ex;
    }

    public void SaveCookie(string strKey, string strValue)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            HttpContext.Current.Request.Cookies[strKey].Value = strValue;
        }
        else
        {
            HttpCookie cookie = new HttpCookie(strKey);
            cookie.Value = strValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public string GetCookie(string strKey)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            return HttpContext.Current.Request.Cookies[strKey].Value;
        }
        else
            return null;
    }

    public void ExpireCookie()
    {
        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;
        int count = req.Cookies.Count;
        for (int i = 0; i < count; i++)
        {
            HttpCookie cookie = new HttpCookie(req.Cookies[i].Name);
            cookie.Expires = DateTime.Now.AddDays(-1);
            res.Cookies.Add(cookie);
        }
    }

}


public static class UIExtensions
{
    public static void EnableCompression(this HttpResponse response)
    {
        response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        response.AddHeader("Content-Encoding", "gzip");
    }
}