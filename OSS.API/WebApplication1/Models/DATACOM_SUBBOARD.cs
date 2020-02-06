using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_SUBBOARD")]
    public class DATACOM_SUBBOARD
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("TYPE_ID")]
        public int TYPE_ID { get; set; }
        [Column("SUBSLOT_ID")]
        public decimal SUBSLOT_ID { get; set; }
        [Column("ALIAS_NAME")]
        public string ALIAS_NAME { get; set; }
        [Column("HARDWARE_VERSION")]
        public string HARDWARE_VERSION { get; set; }
        [Column("SOFTWARE_VERSION")]
        public string SOFTWARE_VERSION { get; set; }
        [Column("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
        [Column("REMARKS")]
        public string REMARKS { get; set; }
        [Column("BARCODE")]
        public string BARCODE { get; set; }
        [Column("STATUS")]
        public string STATUS { get; set; }
        [Column("BOM_ITEM")]
        public string BOM_ITEM { get; set; }
        [Column("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [Column("MANUFACTURE_DATA")]
        public Nullable<System.DateTime> MANUFACTURE_DATA { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        [Column("FREE_PORT")]
        public Nullable<int> FREE_PORT { get; set; }
        [Column("IS_VIRTUAL")]
        public Nullable<short> IS_VIRTUAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_PORT> DATACOM_PORT { get; set; }
        public virtual DATACOM_SUBBOARD_TYPE DATACOM_SUBBOARD_TYPE { get; set; }
    }
}