using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_SUBRACK_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_SUBRACK_TYPE()
        {
            this.OPTICAL_SUBRACK = new HashSet<OPTICAL_SUBRACK>();
        }

        public int ID { get; set; }
        public string TYPE { get; set; }
        public Nullable<int> TOTAL_SLOTS_NUMBER { get; set; }
        public Nullable<int> AVAILABLE_SLOTS_FOR_SERVICES_BOARDS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_SUBRACK> OPTICAL_SUBRACK { get; set; }
    }
}