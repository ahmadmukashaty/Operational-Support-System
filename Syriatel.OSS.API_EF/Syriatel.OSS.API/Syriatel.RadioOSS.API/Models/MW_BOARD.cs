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
    
    public partial class MW_BOARD
    {
        public MW_BOARD()
        {
            this.MW_NE_BOARD = new HashSet<MW_NE_BOARD>();
            this.MW_PORT = new HashSet<MW_PORT>();
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
    
        public virtual MW_BOARD_TYPE MW_BOARD_TYPE { get; set; }
        public virtual ICollection<MW_NE_BOARD> MW_NE_BOARD { get; set; }
        public virtual ICollection<MW_PORT> MW_PORT { get; set; }
    }
}
