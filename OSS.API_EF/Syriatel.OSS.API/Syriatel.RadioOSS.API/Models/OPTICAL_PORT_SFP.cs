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
    
    public partial class OPTICAL_PORT_SFP
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public int CHILD_ID { get; set; }
        public System.DateTime CREATE_DATA { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual OPTICAL_PORT OPTICAL_PORT { get; set; }
        public virtual OPTICAL_SFP OPTICAL_SFP { get; set; }
    }
}
