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
    
    public partial class SITE_IDENTITY
    {
        public SITE_IDENTITY()
        {
            this.CELLs = new HashSet<CELL>();
            this.RAN_CONTROLLER_SITE_IDENTITY = new HashSet<RAN_CONTROLLER_SITE_IDENTITY>();
            this.SITE_IDENTITY_ANTENNA = new HashSet<SITE_IDENTITY_ANTENNA>();
            this.SITE_IDENTITY_RADIO_HOSTVER = new HashSet<SITE_IDENTITY_RADIO_HOSTVER>();
        }
    
        public int ID { get; set; }
        public string NENAME { get; set; }
        public int RADIO_NETYPE_ID { get; set; }
        public string GBTS_TYPE { get; set; }
        public string MBTS_NAME { get; set; }
        public string SITE_VERSION { get; set; }
        public Nullable<System.DateTime> FIRST_CONN_TIME { get; set; }
        public Nullable<System.DateTime> CREATION_TIME { get; set; }
        public string OM_IP { get; set; }
        public string DN { get; set; }
        public string VENDOR { get; set; }
        public string NOTE { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public int SITE_CANDIDATE_ID { get; set; }
        public string SITE_BANDS { get; set; }
        public Nullable<int> SITE_MODE_ID { get; set; }
    
        public virtual ICollection<CELL> CELLs { get; set; }
        public virtual RADIO_NETYPE RADIO_NETYPE { get; set; }
        public virtual ICollection<RAN_CONTROLLER_SITE_IDENTITY> RAN_CONTROLLER_SITE_IDENTITY { get; set; }
        public virtual SITE_CANDIDATE SITE_CANDIDATE { get; set; }
        public virtual ICollection<SITE_IDENTITY_ANTENNA> SITE_IDENTITY_ANTENNA { get; set; }
        public virtual ICollection<SITE_IDENTITY_RADIO_HOSTVER> SITE_IDENTITY_RADIO_HOSTVER { get; set; }
        public virtual SITE_MODE SITE_MODE { get; set; }
    }
}
