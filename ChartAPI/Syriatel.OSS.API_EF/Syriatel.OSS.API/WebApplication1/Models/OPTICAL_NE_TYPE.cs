using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_NE_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_NE_TYPE()
        {
            this.OPTICAL_NE = new HashSet<OPTICAL_NE>();
        }

        public int ID { get; set; }
        public string TYPE { get; set; }
        public string VENDOR { get; set; }
        public string CLASS { get; set; }
        public string SERIES { get; set; }
        public Nullable<int> TOTAL_SUBRACK_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_NE> OPTICAL_NE { get; set; }
    }
}