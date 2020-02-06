using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_NE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_NE()
        {
            this.FIREWALL_NE_BOARD = new HashSet<FIREWALL_NE_BOARD>();
        }

        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public string U2000_REF_ID { get; set; }
        public string NAME { get; set; }
        public string ALIAS_NAME { get; set; }
        public string REMARKS { get; set; }
        public string IP { get; set; }
        public string MAC_ADDRESS { get; set; }
        public string PATCH_VERSION { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> SUB_CATEGORY_ID { get; set; }

        public virtual FIREWALL_NE_TYPE FIREWALL_NE_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_NE_BOARD> FIREWALL_NE_BOARD { get; set; }
    }
}