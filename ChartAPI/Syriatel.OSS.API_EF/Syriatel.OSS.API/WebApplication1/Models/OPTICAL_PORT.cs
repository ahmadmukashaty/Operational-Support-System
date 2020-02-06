using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class OPTICAL_PORT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPTICAL_PORT()
        {
            this.OPTICAL_PORT_SFP = new HashSet<OPTICAL_PORT_SFP>();
        }

        public int ID { get; set; }
        public Nullable<int> TYPE_ID { get; set; }
        public int PORT_ID { get; set; }
        public string PORT_NAME { get; set; }
        public string PORT_LEVEL { get; set; }
        public string RATE_BPS { get; set; }
        public string ALIAS { get; set; }
        public string REMARKS { get; set; }
        public string DESCRIPTION { get; set; }
        public string FIXED_OPTICAL_ATTENUATOR { get; set; }
        public Nullable<int> FIXED_ATTENUATOR_DB { get; set; }
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        public int PARENT_ID { get; set; }

        public virtual OPTICAL_BOARD OPTICAL_BOARD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPTICAL_PORT_SFP> OPTICAL_PORT_SFP { get; set; }
        public virtual OPTICAL_PORT_TYPE OPTICAL_PORT_TYPE { get; set; }
    }
}