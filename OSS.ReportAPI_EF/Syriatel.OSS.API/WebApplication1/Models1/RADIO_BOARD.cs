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
    
    public partial class RADIO_BOARD
    {
        public int ID { get; set; }
        public string BIOS_VEREX { get; set; }
        public Nullable<System.DateTime> DATE_OF_MANUFACTURE { get; set; }
        public string BIOSVER { get; set; }
        public string BOM_CODE { get; set; }
        public string MANUFACTURER_DATA { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string HARDWARE_VERSION { get; set; }
        public string VENDOR_NAME { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public int RADIO_BOARD_TYPE_ID { get; set; }
        public string BOARD_TYPE { get; set; }
        public Nullable<int> SLOT_NUMBER { get; set; }
    
        public virtual RADIO_BOARD_TYPE RADIO_BOARD_TYPE { get; set; }
    }
}