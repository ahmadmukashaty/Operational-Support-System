using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_FINANCE_TERM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_FINANCE_TERM()
        {
            this.OPTICAL_FINANCE_NE = new HashSet<OPTICAL_FINANCE_NE>();
        }

        public int ID { get; set; }
        public Nullable<int> WO_ID { get; set; }
        public Nullable<int> PO_ID { get; set; }
        public Nullable<int> MR_ID { get; set; }
        public Nullable<int> RMR_ID { get; set; }
        public Nullable<int> QOUATATION_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_FINANCE_NE> OPTICAL_FINANCE_NE { get; set; }
    }
}