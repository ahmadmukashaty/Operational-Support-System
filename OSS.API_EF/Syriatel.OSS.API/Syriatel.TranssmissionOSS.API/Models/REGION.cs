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
    
    public partial class REGION
    {
        public REGION()
        {
            this.AREAs = new HashSet<AREA>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string DISPLAY_NAME { get; set; }
    
        public virtual ICollection<AREA> AREAs { get; set; }
    }
}