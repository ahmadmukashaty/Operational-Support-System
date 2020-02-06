using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_BOARD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_BOARD()
        {
            this.OPTICAL_PORT = new HashSet<OPTICAL_PORT>();
            this.OPTICAL_SUBRACK_BOARD = new HashSet<OPTICAL_SUBRACK_BOARD>();
        }

        public int ID { get; set; }
        public int SLOT_ID { get; set; }
        public int NE_ID { get; set; }
        public int TYPE_ID { get; set; }
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

        public virtual OPTICAL_BOARD_TYPE OPTICAL_BOARD_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_PORT> OPTICAL_PORT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
    }
}