using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_AREA_ID_MAPPING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_AREA_ID_MAPPING()
        {
            this.OPTICAL_NE_AREA = new HashSet<OPTICAL_NE_AREA>();
        }

        public int ID { get; set; }
        public string AREA_NAME { get; set; }
        public string AREA_ZONE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_NE_AREA> OPTICAL_NE_AREA { get; set; }
    }
}