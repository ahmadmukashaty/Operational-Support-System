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
    
    public partial class RADIO_BBU
    {
        public int ID { get; set; }
        public string NE_NAME { get; set; }
    
        public virtual RADIO_SUBRACK RADIO_SUBRACK { get; set; }
    }
}