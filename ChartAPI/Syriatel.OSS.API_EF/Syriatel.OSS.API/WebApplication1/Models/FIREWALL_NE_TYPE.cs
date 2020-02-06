using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_NE_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_NE_TYPE()
        {
            this.FIREWALL_NE = new HashSet<FIREWALL_NE>();
        }

        public int ID { get; set; }
        public string VENDOR { get; set; }
        public string CLASS { get; set; }
        public string SERIES { get; set; }
        public string MODEL { get; set; }
        public Nullable<decimal> TOTAL_SLOTS_NUMBERS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_NE> FIREWALL_NE { get; set; }
    }
}