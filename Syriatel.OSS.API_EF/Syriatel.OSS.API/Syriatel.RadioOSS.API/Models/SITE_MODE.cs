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
    
    public partial class SITE_MODE
    {
        public SITE_MODE()
        {
            this.SITE_IDENTITY = new HashSet<SITE_IDENTITY>();
        }
    
        public int ID { get; set; }
        public string S_MODE { get; set; }
        public Nullable<int> RIM_SUBCATEGORY_ID { get; set; }
    
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
        public virtual ICollection<SITE_IDENTITY> SITE_IDENTITY { get; set; }
    }
}
