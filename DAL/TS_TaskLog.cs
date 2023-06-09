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
    
    public partial class TS_TaskLog
    {
        public int TaskLogId { get; set; }
        public int TaskMasterId { get; set; }
        public Nullable<int> TaskDetailId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> AssigneeId { get; set; }
        public string Description { get; set; }
        public bool IsReply { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
        public bool IsActive { get; set; }
        public int CreatedDateInt { get; set; }
        public Nullable<int> ModifiedDateInt { get; set; }
        public Nullable<bool> IsRead { get; set; }
    
        public virtual TS_Setup_TicketStatus TS_Setup_TicketStatus { get; set; }
        public virtual TS_TaskDetail TS_TaskDetail { get; set; }
        public virtual TS_TaskMaster TS_TaskMaster { get; set; }
        public virtual TS_TaskMaster TS_TaskMaster1 { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
