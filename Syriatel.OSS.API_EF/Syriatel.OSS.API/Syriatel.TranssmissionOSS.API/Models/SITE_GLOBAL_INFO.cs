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
    
    public partial class SITE_GLOBAL_INFO
    {
        public SITE_GLOBAL_INFO()
        {
            this.SITE_CANDIDATE = new HashSet<SITE_CANDIDATE>();
        }
    
        public int ID { get; set; }
        public string ENGLISH_NAME { get; set; }
        public string ARABIC_NAME { get; set; }
        public string CODE { get; set; }
        public string LOGITUDE { get; set; }
        public string LATITUDE { get; set; }
        public string AMSL_M { get; set; }
    
        public virtual ICollection<SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
    }
}
