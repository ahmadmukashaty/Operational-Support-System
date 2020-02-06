//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    
    [Table("MW_NE")]
    public partial class MW_NE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MW_NE()
        {
            this.MW_NE_BOARD = new HashSet<MW_NE_BOARD>();
            this.MW_NE_SITE = new HashSet<MW_NE_SITE>();
        }
        [Column("ID")]
        public int ID { get; set; }

        [Column("TYPE_ID")]
        public int TYPE_ID { get; set; }

        [Column("NE_ID")]
        public string NE_ID { get; set; }

        [Column("U2000_REF_ID")]
        public string U2000_REF_ID { get; set; }

        [Column("NAME")]
        public string NAME { get; set; }

        [Column("SUBRACK_TYPE")]
        public string SUBRACK_TYPE { get; set; }

        [Column("GATEWAY_TYPE")]
        public string GATEWAY_TYPE { get; set; }

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
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }

        [Column("CREATE_DATE")]
        public Nullable<System.DateTime> CREATE_DATE { get; set; }

        [Column("SITE_MODEL")]
        public string SITE_MODEL { get; set; }

        [Column("TERMINAL_CLASS")]
        public string TERMINAL_CLASS { get; set; }

        [Column("SUB_CATEGORY_ID")]
        public Nullable<int> SUB_CATEGORY_ID { get; set; }
    
        public virtual ICollection<MW_NE_BOARD> MW_NE_BOARD { get; set; }
        public virtual MW_NE_TYPE MW_NE_TYPE { get; set; }

        public virtual ICollection<MW_NE_SITE> MW_NE_SITE { get; set; }
        public virtual RIM_SUBCATEGORY RIM_SUBCATEGORY { get; set; }
    }
}
