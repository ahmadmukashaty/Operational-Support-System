//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.TranssmissionOSS.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SITE_IDENTITY_ANTENNA
    {
        public int ID { get; set; }
        public int SITE_IDENTITY_ID { get; set; }
        public int ANTENNA_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual ANTENNA ANTENNA { get; set; }
        public virtual SITE_IDENTITY SITE_IDENTITY { get; set; }
    }
}
