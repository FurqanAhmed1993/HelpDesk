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
    
    public partial class Setup_RequestTypeSubcategoryFieldType
    {
        public int RequestTypeSubcategoryFieldTypeId { get; set; }
        public int RequestTypeSubcategoryId { get; set; }
        public string Caption { get; set; }
        public int FieldTypeId { get; set; }
        public string InputClass { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string UserIP { get; set; }
    }
}