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
    
    public partial class OPTICAL_AREA_ID_MAPPING
    {
        public OPTICAL_AREA_ID_MAPPING()
        {
            this.OPTICAL_NE_AREA = new HashSet<OPTICAL_NE_AREA>();
        }
    
        public int ID { get; set; }
        public string AREA_NAME { get; set; }
        public string AREA_ZONE { get; set; }
    
        public virtual ICollection<OPTICAL_NE_AREA> OPTICAL_NE_AREA { get; set; }
    }
}
