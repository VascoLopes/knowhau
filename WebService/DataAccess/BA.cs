//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class BA
    {
        public int baID { get; set; }
        public string adminemail { get; set; }
        public string beaconID { get; set; }
    
        public virtual ADMIN ADMIN { get; set; }
        public virtual BEACON BEACON { get; set; }
    }
}
