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
    
    public partial class RAN_PORT
    {
        public int ID { get; set; }
        public int RAN_BOARD_ID { get; set; }
        public Nullable<int> SUBSLOT_NUMBER { get; set; }
        public Nullable<int> PORT_NUMBER { get; set; }
        public string INVENTORY_UNIT_ID { get; set; }
        public string PORT_TYPE { get; set; }
        public string PORT_DESCRIPTION { get; set; }
        public Nullable<System.DateTime> DATE_OF_LAST_SERVICE { get; set; }
        public Nullable<System.DateTime> DATE_OF_MANUFACTURE { get; set; }
        public string INVENTORY_UNIT_TYPE { get; set; }
        public string MANUFACTURER_DATA { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string SLOT_POSITION { get; set; }
        public string UNIT_POSITION { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_UNIT_TYPE_NUMBER { get; set; }
        public string VERSION_NUMBER { get; set; }
        public string UNIT_POSITION_DETAIL { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
    
        public virtual RAN_BOARD RAN_BOARD { get; set; }
    }
}
