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
    
    public partial class SITE
    {
        public SITE()
        {
            this.DATACOM_NE_SITE = new HashSet<DATACOM_NE_SITE>();
            this.FIREWALL_NE_SITE = new HashSet<FIREWALL_NE_SITE>();
            this.MW_NE_SITE = new HashSet<MW_NE_SITE>();
        }
    
        public int ID { get; set; }
        public string CODE { get; set; }
        public string ENGLISH_NAME { get; set; }
        public string REGION { get; set; }
        public string AREA { get; set; }
        public string ZONE { get; set; }
        public string SUBAREA { get; set; }
    
        public virtual ICollection<DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public virtual ICollection<FIREWALL_NE_SITE> FIREWALL_NE_SITE { get; set; }
        public virtual ICollection<MW_NE_SITE> MW_NE_SITE { get; set; }
    }
}
