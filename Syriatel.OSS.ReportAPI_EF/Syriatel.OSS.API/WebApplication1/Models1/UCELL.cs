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
    
    public partial class UCELL
    {
        public int ID { get; set; }
        public string CELL_NAME { get; set; }
        public string LAC { get; set; }
        public string LOCELL { get; set; }
        public string RAC { get; set; }
        public string SAC { get; set; }
        public string NODEB_NAME { get; set; }
        public string MAXTX_POWER { get; set; }
        public string PSCRAMB_CODE { get; set; }
        public string UARFCN_DOWN_LINK { get; set; }
        public string USER_LABEL { get; set; }
        public string MANUFACTURER_DATA { get; set; }
        public string DETAILS { get; set; }
        public string UARFCN_UP_LINK { get; set; }
    
        public virtual CELL CELL { get; set; }
    }
}
