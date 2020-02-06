using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_SUBBOARD_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FIREWALL_SUBBOARD_TYPE()
        {
            this.FIREWALL_SUBBOARD = new HashSet<FIREWALL_SUBBOARD>();
        }

        public int ID { get; set; }
        public string ALIAS_NAME { get; set; }
        public string TYPE { get; set; }
        public string PORT_TYPE { get; set; }
        public Nullable<decimal> TOTAL_PORTS_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FIREWALL_SUBBOARD> FIREWALL_SUBBOARD { get; set; }
    }
}