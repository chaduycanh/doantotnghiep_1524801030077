//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public long Pid { get; set; }
        public string TeacherCode { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<long> ActiveCode { get; set; }
        public Nullable<int> Lock { get; set; }
    }
}