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
    
    public partial class OPTICAL_PORT
    {
        public OPTICAL_PORT()
        {
            this.OPTICAL_PORT_SFP = new HashSet<OPTICAL_PORT_SFP>();
        }
    
        public int ID { get; set; }
        public Nullable<int> TYPE_ID { get; set; }
        public int PORT_ID { get; set; }
        public string PORT_LEVEL { get; set; }
        public string RATE_BPS { get; set; }
        public string ALIAS { get; set; }
        public string REMARKS { get; set; }
        public string DESCRIPTION { get; set; }
        public string FIXED_OPTICAL_ATTENUATOR { get; set; }
        public Nullable<int> FIXED_ATTENUATOR_DB { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public int PARENT_ID { get; set; }
        public string PORT_NAME { get; set; }
    
        public virtual OPTICAL_BOARD OPTICAL_BOARD { get; set; }
        public virtual OPTICAL_PORT_TYPE OPTICAL_PORT_TYPE { get; set; }
        public virtual ICollection<OPTICAL_PORT_SFP> OPTICAL_PORT_SFP { get; set; }
    }
}
