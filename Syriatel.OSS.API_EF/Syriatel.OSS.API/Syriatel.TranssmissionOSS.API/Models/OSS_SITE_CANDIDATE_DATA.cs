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
    
    public partial class OSS_SITE_CANDIDATE_DATA
    {
        public int SITE_ID { get; set; }
        public string CODE { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public string ARABIC_NAME { get; set; }
        public string ENGLISH_NAME { get; set; }
        public Nullable<short> PHASE { get; set; }
        public Nullable<int> SITE_CATEGORY_ID { get; set; }
        public Nullable<int> SITE_LOCATION_TYPE_ID { get; set; }
        public Nullable<short> CRITICAL_SITE { get; set; }
        public string SITE_OBJECTIVES { get; set; }
        public string SITE_REQUESTEDBY { get; set; }
        public Nullable<decimal> LATITUDE_N { get; set; }
        public Nullable<decimal> LONGITUDE_E { get; set; }
        public Nullable<decimal> AMSL_M { get; set; }
        public Nullable<int> SITE_PROPERTY_TYPE_ID { get; set; }
        public Nullable<int> SITE_KIND_OF_SUPPORT_ID { get; set; }
        public string CANDIDATE_ADDRESS { get; set; }
        public Nullable<int> SITE_CONTACT_PERSON_ID { get; set; }
        public Nullable<int> SUBAREA_ID { get; set; }
        public Nullable<int> CANDIDATE_RANKING { get; set; }
        public Nullable<int> SITE_CO_LOCATED_ID { get; set; }
        public string COVERED_AREA { get; set; }
    }
}
