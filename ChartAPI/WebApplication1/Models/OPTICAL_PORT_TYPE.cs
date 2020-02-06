using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_PORT_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_PORT_TYPE()
        {
            this.OPTICAL_PORT = new HashSet<OPTICAL_PORT>();
        }

        public int ID { get; set; }
        public string TYPE { get; set; }
        public string SUBTYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_PORT> OPTICAL_PORT { get; set; }
    }
}