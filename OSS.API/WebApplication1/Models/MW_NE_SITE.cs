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
    [Table("MW_NE_SITE")]
    public partial class MW_NE_SITE
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("NE_ID")]
        public int NE_ID { get; set; }
        [Column("SITE_ID")]
        public int SITE_ID { get; set; }
        [Column("CREATE_DATE")]
        public System.DateTime CREATE_DATE { get; set; }
        [Column("RETIRE_DATE")]
        public Nullable<System.DateTime> RETIRE_DATE { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
    
        public virtual MW_NE MW_NE { get; set; }
        public virtual SITE SITE { get; set; }
    }
}
