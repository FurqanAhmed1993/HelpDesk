//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    
    public partial class RoleAccess_SelectByApplicationid_Result
    {
        public int Menu_Item_Code { get; set; }
        public string Menu_Item_Name { get; set; }
        public string Menu_URL { get; set; }
        public Nullable<int> Parent_Menu_Item_Code { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<bool> Is_Displayed_In_Menu { get; set; }
        public Nullable<int> User_Code { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string User_IP { get; set; }
        public Nullable<int> Company_Code { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> ApplicationID { get; set; }
        public bool Has_Access { get; set; }
    }
}
