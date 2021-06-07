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
        public int Id { get; set; }

        [Column("TYPE_ID")]
        public int typeId { get; set; }

         [Column("U2000_REF_ID")]
        public int refId { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ALIAS_NAME")]
        public string aliasName { get; set; }

        [Column("SUBRACK_TYPE")]
        public string subrackType { get; set; }

        [Column("IP")]
        public string IP { get; set; }

        [Column("MAC_ADDRESS")]
        public string macAddress { get; set; }
        [Column("SOFTWARE_VERSION")]
        public string softwareVersion { get; set; }
        [Column("PATCH_VERSION")]
        public string patchVersion { get; set; }
        [Column("LSR_ID")]
        public string lsrId { get; set; }
        [Column("REMARKS")]
        public string REMARKS { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> deploymentStatus { get; set; }
        [Column("CREATE_DATE")]
        public Nullable<System.DateTime> createDate { get; set; }
        [Column("FREE_BOARD")]
        public Nullable<int> freeBoard { get; set; }
        [Column("SUB_CATEGORY_ID")]
        public Nullable<int> subCategoryId { get; set; }

        public virtual ICollection<DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
        public virtual ICollection<DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public virtual DATACOM_NE_Type DATACOM_NE_TYPE { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
    }
}