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
    
    public partial class RAN_CONTROLLER_CANDIDATE
    {
        public int ID { get; set; }
        public int SITE_CANDIDATE_ID { get; set; }
        public int RAN_CONTROLLER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual RAN_CONTROLLER RAN_CONTROLLER { get; set; }
        public virtual SITE_CANDIDATE SITE_CANDIDATE { get; set; }
    }
}
