//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.RadioOSS.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SITE_KIND_OF_SUPPORT
    {
        public SITE_KIND_OF_SUPPORT()
        {
            this.SITE_CANDIDATE = new HashSet<SITE_CANDIDATE>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
    }
}