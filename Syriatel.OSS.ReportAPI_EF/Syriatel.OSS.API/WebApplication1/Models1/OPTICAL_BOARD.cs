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
    
    public partial class OPTICAL_BOARD
    {
        public OPTICAL_BOARD()
        {
            this.OPTICAL_PORT = new HashSet<OPTICAL_PORT>();
            this.OPTICAL_SUBRACK_BOARD = new HashSet<OPTICAL_SUBRACK_BOARD>();
        }
    
        public int ID { get; set; }
        public int SLOT_ID { get; set; }
        public int NE_ID { get; set; }
        public int OPTICAL_BOARD_TYPE_ID { get; set; }
        public string HARDWARE_VERSION { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string REMARKS { get; set; }
        public string BARCODE { get; set; }
        public string BIOS_VERSION { get; set; }
        public string FPGA_VERSION { get; set; }
        public string STATUS { get; set; }
        public string BOM_ITEM { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<System.DateTime> MANUFACTURE_DATE { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public Nullable<int> FREE_PORT { get; set; }
        public string TYPENAME { get; set; }
    
        public virtual ICollection<OPTICAL_PORT> OPTICAL_PORT { get; set; }
        public virtual ICollection<OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
    }
}
