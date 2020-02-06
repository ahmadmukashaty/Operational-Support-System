using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_NE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_NE()
        {
            this.OPTICAL_FINANCE_NE = new HashSet<OPTICAL_FINANCE_NE>();
            this.OPTICAL_NE_AREA = new HashSet<OPTICAL_NE_AREA>();
            this.OPTICAL_NE_SUBRACK = new HashSet<OPTICAL_NE_SUBRACK>();
            this.OPTICAL_WO_NE = new HashSet<OPTICAL_WO_NE>();
        }

        public int ID { get; set; }
        public Nullable<int> TYPE_ID { get; set; }
        public string U2000_REF_ID { get; set; }
        public string NAME { get; set; }
        public string ALIAS_NAME { get; set; }
        public string IP { get; set; }
        public string MAC_ADDRESS { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string PATCH_VERSION { get; set; }
        public string LSR_ID { get; set; }
        public string REMARKS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string DEPLOYMENT_STATUS { get; set; }
        public string SHELF_TYPE { get; set; }
        public Nullable<int> FIBER_COUNT { get; set; }
        public string GETWAY_TYPE { get; set; }
        public string MODEL { get; set; }
        public Nullable<int> SUB_CATEGORY_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_FINANCE_NE> OPTICAL_FINANCE_NE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_NE_AREA> OPTICAL_NE_AREA { get; set; }
        public virtual OPTICAL_NE_TYPE OPTICAL_NE_TYPE { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_NE_SUBRACK> OPTICAL_NE_SUBRACK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_WO_NE> OPTICAL_WO_NE { get; set; }
    }
}