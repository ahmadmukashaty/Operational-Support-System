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
    
    public partial class DATACOM_SFP
    {
        public DATACOM_SFP()
        {
            this.DATACOM_PORT_SFP = new HashSet<DATACOM_PORT_SFP>();
        }
    
        public int ID { get; set; }
        public string TYPE { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string SM_MM { get; set; }
        public Nullable<decimal> SPEED { get; set; }
        public Nullable<decimal> WAVELENGTH { get; set; }
        public Nullable<decimal> TRANSMISSION_DISTANCE { get; set; }
        public string FIBER_CABLE_TYPE { get; set; }
        public string MANUFACTURE { get; set; }
        public Nullable<decimal> RECEIVE_POWER { get; set; }
        public string REFERENCE_RECEIVE_POWER { get; set; }
        public Nullable<decimal> REFERENCE_RECEIVE_TIME { get; set; }
        public Nullable<decimal> UPPER_RECEIVE_POWER { get; set; }
        public Nullable<decimal> LOWER_RECEIVE_POWER { get; set; }
        public Nullable<decimal> TRANSMIT_POWER { get; set; }
        public string REFERENCE_TRANSMIT_POWER { get; set; }
        public Nullable<decimal> REFERENCE_TRANSMIT_TIME { get; set; }
        public Nullable<decimal> UPPER_TRANSMIT_POWER { get; set; }
        public Nullable<decimal> LOWER_TRANSMIT_POWER { get; set; }
    
        public virtual ICollection<DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
    }
}