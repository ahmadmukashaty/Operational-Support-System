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
    
    public partial class RIM_LEVEL
    {
        public RIM_LEVEL()
        {
            this.RIM_CATEGORY_LEVEL = new HashSet<RIM_CATEGORY_LEVEL>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string TABLE_NAME { get; set; }
    
        public virtual ICollection<RIM_CATEGORY_LEVEL> RIM_CATEGORY_LEVEL { get; set; }
    }
}
