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
    
    public partial class DATACOM_WORK_ORDER
    {
        public DATACOM_WORK_ORDER()
        {
            this.DATACOM_WO_NE = new HashSet<DATACOM_WO_NE>();
        }
    
        public int WO_ID { get; set; }
        public string WO_DESCRIPTION { get; set; }
    
        public virtual ICollection<DATACOM_WO_NE> DATACOM_WO_NE { get; set; }
    }
}
