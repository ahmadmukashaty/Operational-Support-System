using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_SFP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_SFP()
        {
            this.OPTICAL_PORT_SFP = new HashSet<OPTICAL_PORT_SFP>();
        }

        public int ID { get; set; }
        public string TYPE { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string CLEI_CODE { get; set; }
        public string FIBER_CABLE_TYPE { get; set; }
        public string BOM_CODES { get; set; }
        public string MANUFACTURE { get; set; }
        public Nullable<System.DateTime> MANUFACTURE_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_PORT_SFP> OPTICAL_PORT_SFP { get; set; }
    }
}