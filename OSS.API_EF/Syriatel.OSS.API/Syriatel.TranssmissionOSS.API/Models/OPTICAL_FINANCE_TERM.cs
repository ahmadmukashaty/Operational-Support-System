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
    
    public partial class OPTICAL_FINANCE_TERM
    {
        public OPTICAL_FINANCE_TERM()
        {
            this.OPTICAL_FINANCE_NE = new HashSet<OPTICAL_FINANCE_NE>();
        }
    
        public int ID { get; set; }
        public Nullable<int> WO_ID { get; set; }
        public Nullable<int> PO_ID { get; set; }
        public Nullable<int> MR_ID { get; set; }
        public Nullable<int> RMR_ID { get; set; }
        public Nullable<int> QOUATATION_ID { get; set; }
    
        public virtual ICollection<OPTICAL_FINANCE_NE> OPTICAL_FINANCE_NE { get; set; }
    }
}
