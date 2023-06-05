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
    using System.Collections.Generic;
    
    public partial class Setup_Application
    {
        public Setup_Application()
        {
            this.Roles = new HashSet<Role>();
            this.UserLoginHistories = new HashSet<UserLoginHistory>();
            this.Setup_Application1 = new HashSet<Setup_Application>();
        }
    
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Image { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserLoginHistory> UserLoginHistories { get; set; }
        public virtual ICollection<Setup_Application> Setup_Application1 { get; set; }
        public virtual Setup_Application Setup_Application2 { get; set; }
    }
}
