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
    [Table("MW_PORT")]

    public partial class MW_PORT
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MW_PORT()
        {
            this.MW_PORT_SFP = new HashSet<MW_PORT_SFP>();
        }

        [Column("ID")]

        public int ID { get; set; }
        [Column("TYPE_ID")]

        public int TYPE_ID { get; set; }
        [Column("PORT_ID")]

        public Nullable<int> PORT_ID { get; set; }
        [Column("PORT_NAME")]

        public string PORT_NAME { get; set; }
        [Column("PORT_LEVEL")]

        public string PORT_LEVEL { get; set; }
        [Column("RATE_BPS")]

        public string RATE_BPS { get; set; }
        [Column("DEPLOYMENT_STATUS")]

        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        [Column("REMARK")]

        public string REMARK { get; set; }
        [Column("PARENT_ID")]

        public int PARENT_ID { get; set; }
        [Column("MW_BOARD")]
        public virtual MW_BOARD MW_BOARD { get; set; }
        public virtual MW_PORT_TYPE MW_PORT_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MW_PORT_SFP> MW_PORT_SFP { get; set; }
    }
}
