using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_NE")]
    public class DATACOM_NE
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("TYPE_ID")]
        public int TYPE_ID { get; set; }

         [Column("U2000_REF_ID")]
        public int U2000_REF_ID { get; set; }

        [Column("NAME")]
         public string NAME { get; set; }

        [Column("ALIAS_NAME")]
        public string ALIAS_NAME { get; set; }

        [Column("SUBRACK_TYPE")]
        public string SUBRACK_TYPE { get; set; }

        [Column("IP")]
        public string IP { get; set; }

        [Column("MAC_ADDRESS")]
        public string MAC_ADDRESS { get; set; }
        [Column("SOFTWARE_VERSION")]
        public string SOFTWARE_VERSION { get; set; }
        [Column("PATCH_VERSION")]
        public string PATCH_VERSION { get; set; }
        [Column("LSR_ID")]
        public string LSR_ID { get; set; }
        [Column("REMARKS")]
        public string REMARKS { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        [Column("CREATE_DATE")]
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        [Column("FREE_BOARD")]
        public Nullable<int> FREE_BOARD { get; set; }
        [Column("SUB_CATEGORY_ID")]
        public Nullable<int> SUB_CATEGORY_ID { get; set; }

        public virtual ICollection<DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
        public virtual ICollection<DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public virtual DATACOM_NE_Type DATACOM_NE_TYPE { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
    }
}