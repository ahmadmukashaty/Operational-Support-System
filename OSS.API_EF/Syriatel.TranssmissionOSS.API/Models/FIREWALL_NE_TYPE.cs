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
    
    public partial class FIREWALL_NE_TYPE
    {
        public FIREWALL_NE_TYPE()
        {
            this.FIREWALL_NE = new HashSet<FIREWALL_NE>();
        }
    
        public int ID { get; set; }
        public string VENDOR { get; set; }
        public string CLASS { get; set; }
        public string SERIES { get; set; }
        public string MODEL { get; set; }
        public Nullable<decimal> TOTAL_SLOTS_NUMBERS { get; set; }
    
        public virtual ICollection<FIREWALL_NE> FIREWALL_NE { get; set; }
    }
}