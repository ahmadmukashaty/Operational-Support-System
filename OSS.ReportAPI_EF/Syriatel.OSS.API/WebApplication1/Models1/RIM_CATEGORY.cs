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
    
    public partial class RIM_CATEGORY
    {
        public RIM_CATEGORY()
        {
            this.RIM_CLASSIFICATION_CATEGORY = new HashSet<RIM_CLASSIFICATION_CATEGORY>();
            this.RIM_LEVELS = new HashSet<RIM_LEVELS>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> CAT_ORDER { get; set; }
        public Nullable<int> MODULE_ID { get; set; }
    
        public virtual RIM_MODULES RIM_MODULES { get; set; }
        public virtual ICollection<RIM_CLASSIFICATION_CATEGORY> RIM_CLASSIFICATION_CATEGORY { get; set; }
        public virtual ICollection<RIM_LEVELS> RIM_LEVELS { get; set; }
    }
}
