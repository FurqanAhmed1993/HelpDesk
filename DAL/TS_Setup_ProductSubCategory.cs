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
    
    public partial class TS_Setup_ProductSubCategory
    {
        public int ProductSubCategoryId { get; set; }
        public int TypeOfIssueId { get; set; }
        public string ProductSubCategory { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIp { get; set; }
    
        public virtual TS_Setup_TypeOfIssue TS_Setup_TypeOfIssue { get; set; }
    }
}
