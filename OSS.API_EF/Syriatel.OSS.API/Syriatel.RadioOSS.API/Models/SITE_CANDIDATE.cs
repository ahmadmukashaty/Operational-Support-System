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
    
    public partial class SITE_CANDIDATE
    {
        public SITE_CANDIDATE()
        {
            this.RAN_CONTROLLER_CANDIDATE = new HashSet<RAN_CONTROLLER_CANDIDATE>();
            this.SITE_CANDIDATE_RACK = new HashSet<SITE_CANDIDATE_RACK>();
            this.SITE_IDENTITY = new HashSet<SITE_IDENTITY>();
        }
    
        public int ID { get; set; }
        public Nullable<int> SITE_GLOBAL_INFO_ID { get; set; }
        public string SITE_CODE { get; set; }
        public string ARABIC_NAME { get; set; }
        public string ENGLISH_NAME { get; set; }
        public string SITE_PHASE { get; set; }
        public Nullable<int> SITE_CATEGORY_ID { get; set; }
        public Nullable<int> SITE_LOCATION_TYPE_ID { get; set; }
        public Nullable<short> CRITICAL_SITE { get; set; }
        public Nullable<int> SITE_COVERAGE_TYPE_ID { get; set; }
        public string SITE_OBJECTIVES { get; set; }
        public string SITE_REQUESTEDBY { get; set; }
        public Nullable<int> PREPAREDBY_UID { get; set; }
        public Nullable<System.DateTime> PREPAREDBY_DATE { get; set; }
        public Nullable<int> REVIEWEDBY_UID { get; set; }
        public Nullable<System.DateTime> REVIEWEDBY_DATE { get; set; }
        public Nullable<int> APPROVEDBY_UID { get; set; }
        public Nullable<System.DateTime> APPROVEDBY_DATE { get; set; }
        public string LATITUDE_N { get; set; }
        public string LONGITUDE_E { get; set; }
        public Nullable<int> AMSL_M { get; set; }
        public Nullable<int> SITE_PROPERTY_TYPE_ID { get; set; }
        public Nullable<int> SITE_KIND_OF_SUPPORT_ID { get; set; }
        public Nullable<int> SITE_CONFIGURATION_ID { get; set; }
        public Nullable<int> SUPPORT_HIEGHT { get; set; }
        public string CANDIDATE_ADDRESS { get; set; }
        public Nullable<int> SITE_CONTACT_PERSON_ID { get; set; }
        public Nullable<short> SHELTER_REQUIREMENTS { get; set; }
        public Nullable<int> SUBAREA_ID { get; set; }
        public Nullable<int> CANDIDATE_RANKING { get; set; }
        public Nullable<int> SITE_CO_LOCATED_ID { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public string COVERED_AREA { get; set; }
    
        public virtual ICollection<RAN_CONTROLLER_CANDIDATE> RAN_CONTROLLER_CANDIDATE { get; set; }
        public virtual SITE_COVERAGE_TYPE SITE_COVERAGE_TYPE { get; set; }
        public virtual ICollection<SITE_CANDIDATE_RACK> SITE_CANDIDATE_RACK { get; set; }
        public virtual SITE_CATEGORY SITE_CATEGORY { get; set; }
        public virtual SITE_CO_LOCATED SITE_CO_LOCATED { get; set; }
        public virtual SITE_CONFIGURATION SITE_CONFIGURATION { get; set; }
        public virtual SITE_CONTACT_PERSON SITE_CONTACT_PERSON { get; set; }
        public virtual SITE_GLOBAL_INFO SITE_GLOBAL_INFO { get; set; }
        public virtual SITE_KIND_OF_SUPPORT SITE_KIND_OF_SUPPORT { get; set; }
        public virtual SITE_LOCATION_TYPE SITE_LOCATION_TYPE { get; set; }
        public virtual SITE_PROPERTY_TYPE SITE_PROPERTY_TYPE { get; set; }
        public virtual SUBAREA SUBAREA { get; set; }
        public virtual ICollection<SITE_IDENTITY> SITE_IDENTITY { get; set; }
    }
}