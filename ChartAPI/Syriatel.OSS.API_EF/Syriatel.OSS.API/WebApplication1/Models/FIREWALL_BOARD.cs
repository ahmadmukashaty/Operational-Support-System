using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_BOARD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_BOARD()
        {
            this.FIREWALL_BOARD_SUBBOARD = new HashSet<FIREWALL_BOARD_SUBBOARD>();
            this.FIREWALL_NE_BOARD = new HashSet<FIREWALL_NE_BOARD>();
        }

        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public decimal SUBRACK_ID { get; set; }
        public int? SLOT_ID { get; set; }
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
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }

        public virtual FIREWALL_BOARD_TYPE FIREWALL_BOARD_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_BOARD_SUBBOARD> FIREWALL_BOARD_SUBBOARD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_NE_BOARD> FIREWALL_NE_BOARD { get; set; }
    }
}