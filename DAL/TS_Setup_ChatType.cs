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
    
    public partial class TS_Setup_ChatType
    {
        public TS_Setup_ChatType()
        {
            this.TS_ChatLog = new HashSet<TS_ChatLog>();
        }
    
        public int ChatTypeId { get; set; }
        public string ChatType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<TS_ChatLog> TS_ChatLog { get; set; }
    }
}
