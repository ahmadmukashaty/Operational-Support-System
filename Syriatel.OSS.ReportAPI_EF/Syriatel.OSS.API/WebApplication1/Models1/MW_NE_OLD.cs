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
    
    public partial class MW_NE_OLD
    {
        public MW_NE_OLD()
        {
            this.MW_NE_BOARD_OLD = new HashSet<MW_NE_BOARD_OLD>();
            this.MW_NE_SITE_OLD = new HashSet<MW_NE_SITE_OLD>();
        }
    
        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public string NE_ID { get; set; }
        public string U2000_REF_ID { get; set; }
        public string NAME { get; set; }
        public string SUBRACK_TYPE { get; set; }
        public string GATEWAY_TYPE { get; set; }
        public string IP { get; set; }
        public string MAC_ADDRESS { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string PATCH_VERSION { get; set; }
        public string LSR_ID { get; set; }
        public string REMARKS { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string SITE_MODEL { get; set; }
        public string TERMINAL_CLASS { get; set; }
        public Nullable<int> SUB_CATEGORY_ID { get; set; }
    
        public virtual ICollection<MW_NE_BOARD_OLD> MW_NE_BOARD_OLD { get; set; }
        public virtual ICollection<MW_NE_SITE_OLD> MW_NE_SITE_OLD { get; set; }
    }
}
