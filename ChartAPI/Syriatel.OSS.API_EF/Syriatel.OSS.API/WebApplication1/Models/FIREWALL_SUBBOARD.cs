using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_SUBBOARD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_SUBBOARD()
        {
            this.FIREWALL_BOARD_SUBBOARD = new HashSet<FIREWALL_BOARD_SUBBOARD>();
            this.FIREWALL_CPU = new HashSet<FIREWALL_CPU>();
            this.FIREWALL_PORT = new HashSet<FIREWALL_PORT>();
        }

        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public int SUBSLOT_ID { get; set; }
        public string ALIAS_NAME { get; set; }
        public string HARDWARE_VERSION { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string REMARKS { get; set; }
        public string BARCODE { get; set; }
        public string STATUS { get; set; }
        public string BOM_ITEM { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<System.DateTime> MANUFACTURE_DATE { get; set; }
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        public Nullable<int> FREE_PORT { get; set; }
        public Nullable<short> IS_VIRTUAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_BOARD_SUBBOARD> FIREWALL_BOARD_SUBBOARD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_CPU> FIREWALL_CPU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_PORT> FIREWALL_PORT { get; set; }
        public virtual FIREWALL_SUBBOARD_TYPE FIREWALL_SUBBOARD_TYPE { get; set; }
    }
}