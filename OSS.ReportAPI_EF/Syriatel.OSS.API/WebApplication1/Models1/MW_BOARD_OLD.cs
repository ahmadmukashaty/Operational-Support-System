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
    
    public partial class MW_BOARD_OLD
    {
        public MW_BOARD_OLD()
        {
            this.MW_NE_BOARD_OLD = new HashSet<MW_NE_BOARD_OLD>();
            this.MW_PORT_OLD = new HashSet<MW_PORT_OLD>();
        }
    
        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public string SLOT_ID { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string HARDWARE_VERSION { get; set; }
        public string BIOS_VERSION { get; set; }
        public string FPGA_VERSION { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string BOM_ITEM { get; set; }
        public string BARCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<System.DateTime> MANUFACTURE_DATE { get; set; }
        public string REMARK { get; set; }
    
        public virtual ICollection<MW_NE_BOARD_OLD> MW_NE_BOARD_OLD { get; set; }
        public virtual ICollection<MW_PORT_OLD> MW_PORT_OLD { get; set; }
    }
}
