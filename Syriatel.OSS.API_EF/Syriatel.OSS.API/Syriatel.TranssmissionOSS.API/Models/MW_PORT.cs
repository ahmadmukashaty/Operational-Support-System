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
    
    public partial class MW_PORT
    {
        public MW_PORT()
        {
            this.MW_PORT_SFP = new HashSet<MW_PORT_SFP>();
        }
    
        public int ID { get; set; }
        public int MW_PORT_TYPE_ID { get; set; }
        public Nullable<int> PORT_ID { get; set; }
        public string PORT_NAME { get; set; }
        public string PORT_LEVEL { get; set; }
        public string RATE_BPS { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public string REMARK { get; set; }
        public int MW_BOARD_ID { get; set; }
    
        public virtual MW_BOARD MW_BOARD { get; set; }
        public virtual ICollection<MW_PORT_SFP> MW_PORT_SFP { get; set; }
        public virtual MW_PORT_TYPE MW_PORT_TYPE { get; set; }
    }
}
