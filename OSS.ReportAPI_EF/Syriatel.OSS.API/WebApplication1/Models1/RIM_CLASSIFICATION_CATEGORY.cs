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
    
    public partial class RIM_CLASSIFICATION_CATEGORY
    {
        public int ID { get; set; }
        public Nullable<int> RIM_CATEGORY_ID { get; set; }
        public Nullable<int> RIM_CLASSIFICATION_ID { get; set; }
    
        public virtual RIM_CATEGORY RIM_CATEGORY { get; set; }
    }
}
