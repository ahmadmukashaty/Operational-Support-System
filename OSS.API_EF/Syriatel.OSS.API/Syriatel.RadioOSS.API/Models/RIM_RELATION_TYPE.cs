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
    
    public partial class RIM_RELATION_TYPE
    {
        public RIM_RELATION_TYPE()
        {
            this.RIM_CLASSIFICATION_TABLE = new HashSet<RIM_CLASSIFICATION_TABLE>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<RIM_CLASSIFICATION_TABLE> RIM_CLASSIFICATION_TABLE { get; set; }
    }
}
