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
    
    public partial class TS_Setup_RequestTypeSubcategory
    {
        public TS_Setup_RequestTypeSubcategory()
        {
            this.TS_TicketMaster = new HashSet<TS_TicketMaster>();
        }
    
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public int ResolutionTime { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIp { get; set; }
    
        public virtual TS_Setup_RequestTypeCategory TS_Setup_RequestTypeCategory { get; set; }
        public virtual ICollection<TS_TicketMaster> TS_TicketMaster { get; set; }
    }
}
