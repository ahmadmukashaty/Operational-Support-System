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
    
    public partial class RAN_NETYPE
    {
        public RAN_NETYPE()
        {
            this.RAN_CONTROLLER = new HashSet<RAN_CONTROLLER>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> RIM_SUBCATEGORY_ID { get; set; }
    
        public virtual ICollection<RAN_CONTROLLER> RAN_CONTROLLER { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
    }
}
