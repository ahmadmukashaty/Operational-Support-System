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
    
    public partial class RIM_IS_MAIN
    {
        public int ID { get; set; }
        public Nullable<int> RIM_ATTRIBUTE_ID { get; set; }
        public Nullable<int> RIM_CLASSIFICATION_TABLE_TYPE_ID { get; set; }
        public Nullable<int> PRIORITY { get; set; }
    
        public virtual RIM_ATTRIBUTE RIM_ATTRIBUTE { get; set; }
        public virtual RIM_CLASSIFICATION_TABLE_TYPE RIM_CLASSIFICATION_TABLE_TYPE { get; set; }
    }
}
