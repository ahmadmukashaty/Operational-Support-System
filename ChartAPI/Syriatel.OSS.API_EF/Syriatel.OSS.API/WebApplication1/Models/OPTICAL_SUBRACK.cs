using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_SUBRACK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_SUBRACK()
        {
            this.OPTICAL_NE_SUBRACK = new HashSet<OPTICAL_NE_SUBRACK>();
            this.OPTICAL_SUBRACK_BOARD = new HashSet<OPTICAL_SUBRACK_BOARD>();
        }

        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> RACK_ID { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string ALIAS { get; set; }
        public string REMARKS { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public Nullable<int> FREE_BOARD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_NE_SUBRACK> OPTICAL_NE_SUBRACK { get; set; }
        public virtual OPTICAL_RACK OPTICAL_RACK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
        public virtual OPTICAL_SUBRACK_TYPE OPTICAL_SUBRACK_TYPE { get; set; }
    }
}