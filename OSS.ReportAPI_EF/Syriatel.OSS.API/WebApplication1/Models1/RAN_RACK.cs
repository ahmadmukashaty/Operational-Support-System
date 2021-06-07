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
    
    public partial class RAN_RACK
    {
        public RAN_RACK()
        {
            this.RAN_CONTROLLER_RACK = new HashSet<RAN_CONTROLLER_RACK>();
            this.RAN_RACK_SUBRACK = new HashSet<RAN_RACK_SUBRACK>();
        }
    
        public int ID { get; set; }
        public Nullable<int> RACK_NUMBER { get; set; }
        public string BOM_RACK_TYPE { get; set; }
        public Nullable<System.DateTime> DATE_OF_LAST_SERVICE { get; set; }
        public Nullable<System.DateTime> DATE_OF_MANUFACTURE { get; set; }
        public Nullable<int> INVENTORY_UNIT_ID { get; set; }
        public Nullable<int> ISSUE_NUMBER { get; set; }
        public string INVENTORY_UNIT_TYPE { get; set; }
        public string BOM_CODE { get; set; }
        public string MANUFACTURER_DATA { get; set; }
        public string RACK_TYPE { get; set; }
        public Nullable<int> SERIAL_NUMBER { get; set; }
        public string UNIT_POSITION { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_UNIT_FAMILY_TYPE { get; set; }
        public string VENDOR_UNIT_TYPE_NUMBER { get; set; }
        public string VERSION_NUMBER { get; set; }
        public string UNIT_POSITION_DETAIL { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
    
        public virtual ICollection<RAN_CONTROLLER_RACK> RAN_CONTROLLER_RACK { get; set; }
        public virtual ICollection<RAN_RACK_SUBRACK> RAN_RACK_SUBRACK { get; set; }
    }
}
