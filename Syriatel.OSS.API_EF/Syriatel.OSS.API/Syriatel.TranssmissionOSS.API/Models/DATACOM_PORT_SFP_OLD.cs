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
    
    public partial class DATACOM_PORT_SFP_OLD
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public int CHILD_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
    
        public virtual DATACOM_PORT_OLD DATACOM_PORT_OLD { get; set; }
        public virtual DATACOM_SFP_OLD DATACOM_SFP_OLD { get; set; }
    }
}
