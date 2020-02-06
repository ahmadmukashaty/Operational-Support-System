using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_PORT")]
    public class DATACOM_PORT
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("TYPE_ID")]
        public int TYPE_ID { get; set; }
        [Column("PORT_ID")]
        public int PORT_ID { get; set; }
        [Column("PORT_NAME")]

        public string PORT_NAME { get; set; }
        [Column("SUB_TYPE")]
        public string SUB_TYPE { get; set; }
        [Column("RATE_BPS")]
        public string RATE_BPS { get; set; }
        [Column("PORT_LEVEL")]
        public string PORT_LEVEL { get; set; }
        [Column("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        [Column("PARENT_ID")]
        public int PARENT_ID { get; set; }

        public virtual DATACOM_PORT_TYPE DATACOM_PORT_TYPE { get; set; }
        public virtual DATACOM_SUBBOARD DATACOM_SUBBOARD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
    }
}