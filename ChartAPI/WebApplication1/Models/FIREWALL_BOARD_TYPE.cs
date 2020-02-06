using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_BOARD_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_BOARD_TYPE()
        {
            this.FIREWALL_BOARD = new HashSet<FIREWALL_BOARD>();
        }

        public int ID { get; set; }
        public string ALIAS_NAME { get; set; }
        public string TYPE { get; set; }
        public Nullable<decimal> TOTAL_SUBSLOTS_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_BOARD> FIREWALL_BOARD { get; set; }
    }
}