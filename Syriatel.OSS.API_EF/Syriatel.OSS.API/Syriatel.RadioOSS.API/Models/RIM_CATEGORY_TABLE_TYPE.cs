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
    
    public partial class RIM_CATEGORY_TABLE_TYPE
    {
        public RIM_CATEGORY_TABLE_TYPE()
        {
            this.RIM_IS_MAIN = new HashSet<RIM_IS_MAIN>();
        }
    
        public int ID { get; set; }
        public Nullable<int> RIM_CATEGORY_TABLE_ID { get; set; }
        public Nullable<int> RIM_TYPE_ID { get; set; }
        public Nullable<int> LEVEL_ORDER { get; set; }
        public string DISPLAY_NAME { get; set; }
    
        public virtual RIM_CATEGORY_TABLE RIM_CATEGORY_TABLE { get; set; }
        public virtual RIM_TYPE RIM_TYPE { get; set; }
        public virtual ICollection<RIM_IS_MAIN> RIM_IS_MAIN { get; set; }
    }
}
