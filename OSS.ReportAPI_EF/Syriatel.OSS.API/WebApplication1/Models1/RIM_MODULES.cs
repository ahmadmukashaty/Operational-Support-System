//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.OSS.API.Models1
{
    using System;
    using System.Collections.Generic;
    
    public partial class RIM_MODULES
    {
        public RIM_MODULES()
        {
            this.RIM_CATEGORY = new HashSet<RIM_CATEGORY>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<RIM_CATEGORY> RIM_CATEGORY { get; set; }
    }
}
