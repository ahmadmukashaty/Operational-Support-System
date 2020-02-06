using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_BOARD_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_BOARD_TYPE()
        {
            this.OPTICAL_BOARD = new HashSet<OPTICAL_BOARD>();
        }

        public int ID { get; set; }
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public string ALIAS { get; set; }
        public int RESERVED_SLOTS { get; set; }
        public int TOTAL_PORTS_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_BOARD> OPTICAL_BOARD { get; set; }
    }
}